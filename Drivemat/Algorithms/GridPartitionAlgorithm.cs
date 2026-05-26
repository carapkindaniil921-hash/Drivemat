using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriverSearchApp.Models;

namespace DriverSearchApp.Algorithms;

public class GridPartitionAlgorithm : INearestDriverFinder
{
    private readonly int _gridSize; 
    private Dictionary<int, List<Driver>> _grid = new();
    public GridPartitionAlgorithm(int gridSize = 10) 
    {
        _gridSize = gridSize;
    }
    public void BuildGrid(List<Driver> drivers, int maxX, int maxY)
    {
        _grid.Clear();
        foreach (var driver in drivers)
        {
            int cellX = driver.X / _gridSize;
            int cellY = driver.Y / _gridSize;
            int cellKey = cellX * 10000 + cellY;
            if (!_grid.ContainsKey(cellKey))
                _grid[cellKey] = new List<Driver>();
            _grid[cellKey].Add(driver);
        }
    }
    public List<Driver> FindNearest(List<Driver> drivers, int orderX, int orderY, int topN)
    {
        if (_grid.Count == 0)
            return new BruteForceAlgorithm().FindNearest(drivers, orderX, orderY, topN);

        int cellX = orderX / _gridSize;
        int cellY = orderY / _gridSize;
        var candidates = new List<Driver>();
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                int key = (cellX + dx) * 10000 + (cellY + dy);
                if (_grid.TryGetValue(key, out var cellDrivers))
                    candidates.AddRange(cellDrivers);
            }
        }
        if (candidates.Count < topN)
            candidates = drivers;
        return candidates
            .Select(d => new { Driver = d, DistSq = (d.X - orderX) * (d.X - orderX) + (d.Y - orderY) * (d.Y - orderY) })
            .OrderBy(x => x.DistSq)
            .Take(topN)
            .Select(x => x.Driver)
            .ToList();
    }
}
