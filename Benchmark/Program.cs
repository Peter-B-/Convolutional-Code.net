using BenchmarkDotNet.Running;

namespace Benchmark
{
    internal class BenchmarkLauncher
    {
        private static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<DecodeBenchmark>();
        }
    }
}