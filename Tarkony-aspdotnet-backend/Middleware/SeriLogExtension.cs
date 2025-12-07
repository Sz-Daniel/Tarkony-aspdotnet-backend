using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

public static class SeriLogExtension
{
    public static ConfigureHostBuilder UseSeriLogLogging(this ConfigureHostBuilder services)
    {
        services.UseSerilog(
            (ctx, services, cfg) =>
            {
                cfg.ReadFrom
                    .Configuration(ctx.Configuration)
                    .ReadFrom
                    .Services(services)
                    .Enrich
                    .FromLogContext()
                    .WriteTo
                    .Console()
                    .WriteTo
                    .File("logs/app-.log", rollingInterval: RollingInterval.Day);
            }
        );
        return services;
    }
}
