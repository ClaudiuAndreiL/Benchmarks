using BenchmarkDotNet.Running;
using Benchmarks.DeniedContent.GraphVersion;
using Benchmarks.DeniedContent.GraphVersion2;
using Benchmarks.DeniedContent.RegexVersion;

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
            //var deniedSenders = new List<string> { "inner", "imer", "komikon", "entry" };

            //var regexStuff = new RegexGenerator();
            //regexStuff.Generate(deniedSenders);
            //var result = regexStuff.Validate("info");
            //var result1 = regexStuff.Validate("1rneria");
            //var result2 = regexStuff.Validate("b1rneria");
            //var result3 = regexStuff.Validate("bimer");
            //var result4 = regexStuff.Validate("b1nneria");

            //var graphStuff = new GraphGenerator();
            //graphStuff.Generate(deniedSenders);
            //var graphResult = graphStuff.Search("1nf0");
            //var graphResult1 = graphStuff.Search("1rneria");
            //var graphResult2 = graphStuff.Search("b1rneria");
            //var graphResult3 = graphStuff.Search("bimer");
            //var graphResult4 = graphStuff.Search("b1nneria");

            //var graphStuff2 = new GraphGenerator2();
            //graphStuff2.Generate(deniedSenders);
            //var graph2Result = graphStuff2.Search("1nf0");
            //var graph2Result1 = graphStuff2.Search("1rneria"); //
            //var graph2Result2 = graphStuff2.Search("b1rneria"); //
            //var graph2Result3 = graphStuff2.Search("bimer");
            //var graph2Result4 = graphStuff2.Search("b1nneria"); //

            //var ceva = "asd";

            //dotnet run -c Release --filter *DeniedSenderMatchingBenchmark*
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}