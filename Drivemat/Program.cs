using BenchmarkDotNet.Running;
using DriverSearchApp.Benchmarks;

namespace DriverSearchApp;

public class Program
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<NearestDriverBenchmarks>();
    }
}