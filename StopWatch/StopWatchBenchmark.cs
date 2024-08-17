using BenchmarkDotNet.Attributes;
using System.Diagnostics;

namespace Benchmarks.StopWatch
{
    [MemoryDiagnoser]
    public class StopWatchBenchmark
    {
        StopwatchHelperFactory Factory;

        public StopWatchBenchmark()
        {
            Factory = new StopwatchHelperFactory();
        }

        [Params(1, 10, 100)]
        public int N;

        private int RunCount = 0;

        [GlobalSetup]
        public void Setup()
        {
            RunCount = N;
        }

        [Benchmark]
        public void DoStuff()
        {
            for (int i = 0; i < RunCount; i++)
            {
                var sw = Stopwatch.StartNew();
                var duration = sw.ElapsedMilliseconds;
            }
        }


        [Benchmark]
        public void DoStuffUsing()
        {
            for (int i = 0; i < RunCount; i++)
            {
                double duration = 0;
                using var sw = Factory.Create(elapsed => duration = elapsed.TotalMilliseconds);

            }
        }
    }
}
