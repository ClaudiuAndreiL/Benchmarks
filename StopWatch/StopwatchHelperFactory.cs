namespace Benchmarks.StopWatch;

/// <summary>
/// allows to measure time in a disposable scope
/// </summary>
public interface IStopwatchHelperFactory
{
    IStopwatchHelper Create();
    IStopwatchHelper Create(Action<TimeSpan>? measurementAction);
}

public class StopwatchHelperFactory : IStopwatchHelperFactory
{
    public IStopwatchHelper Create()
    {
        return new StopwatchHelper();
    }

    /// <summary>
    /// creates a new instance of <see cref="IStopwatchHelper"/> and specifies what happens when the measurement scope is disposed
    /// </summary>
    /// <param name="measurementAction"></param>
    /// <returns></returns>
    public IStopwatchHelper Create(Action<TimeSpan>? measurementAction)
    {
        return new StopwatchHelper(measurementAction);
    }
}

