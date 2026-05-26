using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriverSearchApp.Models;

namespace DriverSearchApp;

public static class DataGenerator
{
    private static readonly Random _random = new Random(42);
    public static List<Driver> GenerateDrivers(int count, int maxX, int maxY)
    {
        var drivers = new List<Driver>(count);
        for (int i = 0; i < count; i++)
        {
            drivers.Add(new Driver
            {
                Id = i,
                X = _random.Next(0, maxX),
                Y = _random.Next(0, maxY)
            });
        }
        return drivers;
    }
}