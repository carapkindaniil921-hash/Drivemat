using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriverSearchApp.Models;
using System.Collections.Generic;

namespace DriverSearchApp.Algorithms;

public class SortedSetAlgorithm : INearestDriverFinder
{
    public List<Driver> FindNearest(List<Driver> drivers, int orderX, int orderY, int topN)
    {
        var nearest = new SortedSet<(double dist, Driver driver)>(Comparer<(double dist, Driver driver)>.Create((a, b) =>
        {
            int cmp = a.dist.CompareTo(b.dist);
            if (cmp != 0) return cmp;
            return a.driver.Id.CompareTo(b.driver.Id);
        }));
        foreach (var driver in drivers)
        {
            double dist = Math.Sqrt((driver.X - orderX) * (driver.X - orderX) + (driver.Y - orderY) * (driver.Y - orderY));
            if (nearest.Count < topN)
            {
                nearest.Add((dist, driver));
            }
            else if (dist < nearest.Max.dist)
            {
                nearest.Remove(nearest.Max);
                nearest.Add((dist, driver));
            }
        }
        return nearest.Select(x => x.driver).ToList();
    }
}
