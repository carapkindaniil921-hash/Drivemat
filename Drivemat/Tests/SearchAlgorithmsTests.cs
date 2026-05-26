using DriverSearchApp.Algorithms;
using DriverSearchApp.Models;
using NUnit.Framework;
using System.Linq;

namespace DriverSearchApp.Tests;

[TestFixture]
public class SearchAlgorithmsTests
{
    private List<Driver> _testDrivers = null!;
    private BruteForceAlgorithm _bruteForce = null!;
    private GridPartitionAlgorithm _gridAlgorithm = null!;
    private SortedSetAlgorithm _sortedSetAlgorithm = null!;

    [SetUp]
    public void Setup()
    {
        _testDrivers = new List<Driver>
        {
            new Driver { Id = 1, X = 0, Y = 0 },
            new Driver { Id = 2, X = 10, Y = 0 },
            new Driver { Id = 3, X = 5, Y = 5 },
            new Driver { Id = 4, X = 1, Y = 1 },
            new Driver { Id = 5, X = 100, Y = 100 }
        };

        _bruteForce = new BruteForceAlgorithm();
        _gridAlgorithm = new GridPartitionAlgorithm(10);
        _sortedSetAlgorithm = new SortedSetAlgorithm();

        _gridAlgorithm.BuildGrid(_testDrivers, 200, 200);
    }

    [Test]
    public void Test_BruteForce_ReturnsCorrectNearest()
    {
        var result = _bruteForce.FindNearest(_testDrivers, 0, 0, 2);

        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result[0].Id, Is.EqualTo(1));
        Assert.That(result[1].Id, Is.EqualTo(4));
    }

    [Test]
    public void Test_GridAlgorithm_MatchesBruteForce()
    {
        var bruteResult = _bruteForce.FindNearest(_testDrivers, 5, 5, 3);
        var gridResult = _gridAlgorithm.FindNearest(_testDrivers, 5, 5, 3);

        Assert.That(gridResult.Count, Is.EqualTo(bruteResult.Count));
        for (int i = 0; i < bruteResult.Count; i++)
        {
            Assert.That(gridResult[i].Id, Is.EqualTo(bruteResult[i].Id));
        }
    }

    [Test]
    public void Test_SortedSetAlgorithm_MatchesBruteForce()
    {
        var bruteResult = _bruteForce.FindNearest(_testDrivers, 2, 2, 2);
        var sortedResult = _sortedSetAlgorithm.FindNearest(_testDrivers, 2, 2, 2);

        Assert.That(sortedResult.Count, Is.EqualTo(bruteResult.Count));
        Assert.That(sortedResult.First().Id, Is.EqualTo(bruteResult.First().Id));
    }
}