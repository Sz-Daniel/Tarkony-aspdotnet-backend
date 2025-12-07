public static class CustomMiddlewareExtensions
{
    public static IApplicationBuilder UseLoggerExtension(this IApplicationBuilder app)
    {
        app.Use(
            async (context, next) =>
            {
                var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogInformation(
                    "[{Method} {Path} {UtcNow}] Start.",
                    context.Request.Method,
                    context.Request.Path,
                    DateTime.UtcNow
                );
                await next(context);
                logger.LogInformation(
                    "[{Method} {Path} {UtcNow}] End.",
                    context.Request.Method,
                    context.Request.Path,
                    DateTime.UtcNow
                );
            }
        );

        app.Use(
            async (ctx, next) =>
            {
                var traceId = ctx.TraceIdentifier;
                using (Serilog.Context.LogContext.PushProperty("TraceId", traceId))
                {
                    await next();
                }
            }
        );
        return app;
    }
}
