namespace Pilgaard.BackgroundJobs;

/// <summary>
/// Represents a registration for a background job.
/// </summary>
public sealed class BackgroundJobRegistration
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BackgroundJobRegistration"/> class.
    /// </summary>
    /// <param name="instance">The instance of the background job.</param>
    /// <param name="name">The name of the background job.</param>
    /// <param name="timeout">The timeout for the background job. (optional)</param>
    public BackgroundJobRegistration(
        IBackgroundJob instance,
        string name,
        TimeSpan? timeout = null)
    {
        if (timeout <= TimeSpan.Zero && timeout != System.Threading.Timeout.InfiniteTimeSpan)
            throw new ArgumentOutOfRangeException(nameof(timeout));


        Name = name ?? throw new ArgumentNullException(nameof(name));

        Factory = (_) => instance;

        Timeout = timeout ?? System.Threading.Timeout.InfiniteTimeSpan;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BackgroundJobRegistration"/> class.
    /// </summary>
    /// <param name="factory">The factory used to create the background job instance.</param>
    /// <param name="name">The name of the background job.</param>
    /// <param name="timeout">The timeout for the background job. (optional)</param>
    public BackgroundJobRegistration(
        Func<IServiceProvider, IBackgroundJob> factory,
        string name,
        TimeSpan? timeout = null)
    {
        if (timeout <= TimeSpan.Zero && timeout != System.Threading.Timeout.InfiniteTimeSpan)
            throw new ArgumentOutOfRangeException(nameof(timeout));

        Name = name ?? throw new ArgumentNullException(nameof(name));

        Factory = factory ?? throw new ArgumentNullException(nameof(factory));

        Timeout = timeout ?? System.Threading.Timeout.InfiniteTimeSpan;
    }

    /// <summary>
    /// Gets a delegate used to create the <see cref="IBackgroundJob"/> instance.
    /// </summary>
    public Func<IServiceProvider, IBackgroundJob> Factory { get; }

    /// <summary>
    /// Gets the job name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the timeout used for the job.
    /// </summary>
    public TimeSpan Timeout { get; }
}
