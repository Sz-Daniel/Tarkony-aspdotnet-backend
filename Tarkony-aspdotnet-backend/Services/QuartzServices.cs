using Quartz;
using Mongo.Services;
public interface IQuartzJobScheduler
{
    Task ScheduleJobAsync<TJob>(TimeSpan delay, string userId, string message, CancellationToken cancellationToken) where TJob : IJob;
    
}

public class QuartzJobScheduler(ISchedulerFactory schedulerFactory, ILogger<QuartzJobScheduler> logger) : IQuartzJobScheduler
{
    private readonly ISchedulerFactory _schedulerFactory = schedulerFactory;
    private IScheduler? _scheduler;
    private readonly ILogger<QuartzJobScheduler> _logger = logger;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
        await _scheduler.Start(cancellationToken);
    }

    public async Task ScheduleJobAsync<TJob>(TimeSpan delay, string userId, string message, CancellationToken cancellationToken) where TJob : IJob
    {
        _logger.LogInformation("Scheduling job {JobName} for user {UserId} with message {Message}", typeof(TJob).Name, userId, message);

        var jobDetail = JobBuilder.Create<TJob>()
            .WithIdentity($"{typeof(TJob).Name}-{userId}")
            .UsingJobData("userId", userId)
            .UsingJobData("message", message)
            .Build();

        var scheduler = await _schedulerFactory.GetScheduler();
        var trigger = TriggerBuilder.Create()
            .StartAt(DateTimeOffset.Now.Add(delay))
            .Build();

        await scheduler.ScheduleJob(jobDetail, trigger, cancellationToken);
    }
}



public class PriceUpdate : IJob
{
    private readonly MongoDBService _service;

    // DI-vel be tudod injektálni a service-t
    public PriceUpdate(MongoDBService service)
    {
        _service = service;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        // Itt futtatod a függvényt, amit időzíteni akarsz
        await _service.FetchItemUploadAsync();

        // vagy bármilyen más logika
        Console.WriteLine($"Job lefutott: {DateTime.Now}");
    }
}

public class DataRefresh : IJob
{
    private readonly MongoDBService _service;

    // DI-vel be tudod injektálni a service-t
    public DataRefresh(MongoDBService service)
    {
        _service = service;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        // Itt futtatod a függvényt, amit időzíteni akarsz
        await _service.FetchItemUploadAsync();

        // vagy bármilyen más logika
        Console.WriteLine($"Job lefutott: {DateTime.Now}");
    }
}
