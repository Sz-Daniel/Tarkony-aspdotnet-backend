using Quartz;

public static class QuartzExtension
{
    public static IServiceCollection AddQuartzJobs(this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService(opt =>
        {
            opt.WaitForJobsToComplete = true;
        });

        services.AddSingleton<IQuartzJobScheduler, QuartzJobScheduler>();

        return services;
    }
}
