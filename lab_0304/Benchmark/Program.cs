

using Benchmark.Compare;
using BenchmarkDotNet.Running;

class Program
{
    static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<Compare>();
        Console.WriteLine(summary);
    }
}
