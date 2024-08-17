using System.Diagnostics;

namespace Benchmarks.StopWatch;

public interface IStopwatchHelper : IDisposable
{
    void SetMeasurementAction(Action<TimeSpan>? value);
}

//TODO: consider pushing this to Api.Core
public sealed class StopwatchHelper : IStopwatchHelper
{
    private readonly Stopwatch _stopwatch;
    private Action<TimeSpan>? _measurementAction;

    public StopwatchHelper()
    {
        _stopwatch = Stopwatch.StartNew();
    }

    public StopwatchHelper(Action<TimeSpan>? measurementAction) : this()
    {
        _measurementAction = measurementAction;
    }

    public void SetMeasurementAction(Action<TimeSpan>? value)
    {
        _measurementAction = value;
    }

    public void Dispose()
    {
        _measurementAction?.Invoke(Elapsed);
    }

    public TimeSpan Elapsed => _stopwatch.Elapsed;
}

