using System.Net.Http;
using System.Threading.RateLimiting;
using GraphQL;
using Microsoft.AspNetCore.RateLimiting;

public static class RateLimitExtension
{
    public static IServiceCollection AddRateLimiterExtension(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            // Globális limit minden endpointra
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(
                httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.User?.Identity?.Name
                            ?? httpContext.Connection.RemoteIpAddress?.ToString()
                            ?? "anonymous",
                        factory: partition =>
                            new FixedWindowRateLimiterOptions
                            {
                                PermitLimit = 100, // engedélyezett kérések száma
                                Window = TimeSpan.FromMinutes(1), // időablak
                                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                                QueueLimit = 0
                            }
                    )
            );
            options.AddFixedWindowLimiter(
                "frontend",
                opt =>
                {
                    opt.PermitLimit = 10; // max 10 kérés
                    opt.Window = TimeSpan.FromSeconds(30);
                    opt.QueueLimit = 0;
                }
            );

            options.OnRejected = (context, token) =>
            {
                context.HttpContext.Response.WriteAsJsonAsync(new { error = "Too many requests" });
                return new ValueTask();
            };
        });
        return services;
    }
}
