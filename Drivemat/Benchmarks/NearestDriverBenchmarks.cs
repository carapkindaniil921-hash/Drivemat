using BenchmarkDotNet.Attributes;
using DriverSearchApp.Algorithms;
using DriverSearchApp.Models;

namespace DriverSearchApp.Benchmarks;

[MemoryDiagnoser]
[SimpleJob(launchCount: 1, warmupCount: 1, iterationCount: 3)]
public class NearestDriverBenchmarks
{
    private List<Driver> _drivers = null!;
    private BruteForceAlgorithm _bruteForce = null!;
    private GridPartitionAlgorithm _gridAlgorithm = null!;
    private SortedSetAlgorithm _sortedSetAlgorithm = null!;
    private readonly int _orderX = 500;
    private readonly int _orderY = 500;

    [GlobalSetup]
    public void Setup()
    {
        _drivers = DataGenerator.GenerateDrivers(100_000, 1000, 1000);

        _bruteForce = new BruteForceAlgorithm();
        _gridAlgorithm = new GridPartitionAlgorithm(20);
        _sortedSetAlgorithm = new SortedSetAlgorithm();

        _gridAlgorithm.BuildGrid(_drivers, 1000, 1000);
    }

    [Benchmark(Baseline = true)]
    public List<Driver> BruteForce() => _bruteForce.FindNearest(_drivers, _orderX, _orderY, 5);

    [Benchmark]
    public List<Driver> GridPartition() => _gridAlgorithm.FindNearest(_drivers, _orderX, _orderY, 5);

    [Benchmark]
    public List<Driver> SortedSet() => _sortedSetAlgorithm.FindNearest(_drivers, _orderX, _orderY, 5);
}