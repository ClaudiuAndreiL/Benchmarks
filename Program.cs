using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Benchmarks.PhoneNumberParsing;
using Benchmarks.SerializationBenchmarks;

namespace Benchmarks
{
    // dotnet run -c Release
    public class Program
    {
        /// <summary>
        /// Run using
        /// dotnet run -c Release --filter *SerializationBenchmark* using the desired benchmark
        /// 
        /// Available Benchmarks:
        /// #0 MaxAllowedMessagePartsBenchmark
        /// #1 PhoneNumberBenchmark
        /// #2 DeserializationBenchmarks
        /// #3 SerializationBenchmark
        /// #4 SerializeDeserializeBenchmark
        /// #5 StopWatchBenchmark
        /// 
        /// or run all with
        /// dotnet run -c Release -- --filter *
        /// </summary>
        /// <param name="args"></param>

        public static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

        }
    }
}