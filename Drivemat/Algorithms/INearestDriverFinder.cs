using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriverSearchApp.Models;

namespace DriverSearchApp.Algorithms;

public interface INearestDriverFinder
{
    List<Driver> FindNearest(List<Driver> drivers, int orderX, int orderY, int topN);
}
