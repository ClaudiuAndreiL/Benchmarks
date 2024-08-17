Run using 

RUN ALL
dotnet run -c Release -- --filter *


dotnet run -c Release -- --filter MaxAllowedMessagePartsBenchmark
/// Available Benchmarks:
/// #0 MaxAllowedMessagePartsBenchmark
/// #1 PhoneNumberBenchmark
/// #2 DeserializationBenchmarks
/// #3 SerializationBenchmark
/// #4 SerializeDeserializeBenchmark
/// #5 StopWatchBenchmark

E.g. dotnet run -c Release -- --filter *MaxAllowedMessageParts*

View list of available benchmarks:
dotnet run -c Release -- --list flat
dotnet run -c Release -- --list tree
