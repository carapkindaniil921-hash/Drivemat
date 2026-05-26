using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriverSearchApp.Models;

namespace DriverSearchApp.Algorithms;

public class BruteForceAlgorithm : INearestDriverFinder
{
    public List<Driver> FindNearest(List<Driver> drivers, int orderX, int orderY, int topN)
    {
        return drivers
            .Select(d => new { Driver = d, DistSq = (d.X - orderX) * (d.X - orderX) + (d.Y - orderY) * (d.Y - orderY) })
            .OrderBy(x => x.DistSq)
            .Take(topN)
            .Select(x => x.Driver)
            .ToList();
    }
}
