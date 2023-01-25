namespace Pilgaard.BackgroundJobs;

/// <summary>
/// A simple implementation of <see cref="IRecurringJob"/> which uses a provided delegate to
/// implement the job.
/// </summary>
internal sealed class DelegateRecurringJob : IRecurringJob
{
    private readonly Func<CancellationToken, Task> _job;

    public TimeSpan Interval { get; }

    public DelegateRecurringJob(Func<CancellationToken, Task> job, TimeSpan interval)
    {
        _job = job ?? throw new ArgumentNullException(nameof(job));
        Interval = interval;
    }

    public Task RunJobAsync(CancellationToken cancellationToken = default)
        => _job(cancellationToken);
}
