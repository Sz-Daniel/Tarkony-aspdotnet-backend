public static class CustomMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app)
    {
        app.Use(
            async (context, next) =>
            {
                Console.WriteLine(
                    $"[{context.Request.Method} {context.Request.Path} {DateTime.UtcNow}] Called."
                );
                await next(context);
            }
        );
        return app;
    }
}
