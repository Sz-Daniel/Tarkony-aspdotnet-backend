using System.Net.Http;
using GraphQL;

public static class GraphQLExtension
{
    public static IServiceCollection AddGraphQL(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        var apiUrl = config["THIRDPARTY:API"];
        Console.WriteLine($"API URL: {apiUrl}");
        if (string.IsNullOrEmpty(apiUrl))
            throw new InvalidOperationException(
                "THIRDPARTY__API environment variable must be set."
            );

        services.AddTransient<RetryHandler>();
        services
            .AddHttpClient(
                "GraphQLClient",
                client =>
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.Timeout = TimeSpan.FromSeconds(30);
                }
            )
            .AddHttpMessageHandler<RetryHandler>();
        services.AddScoped<GraphQLService>();
        return services;
    }
}
