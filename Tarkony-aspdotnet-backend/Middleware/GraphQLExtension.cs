using System.Net.Http;
using GraphQL;

public static class GraphQLExtension
{
    public static IServiceCollection AddGraphQL(this IServiceCollection services)
    {
        services.AddTransient<RetryHandler>();
        services
            .AddHttpClient(
                "GraphQLClient",
                client =>
                {
                    client.BaseAddress = new Uri("https://api.tarkov.dev/graphql");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.Timeout = TimeSpan.FromSeconds(30);
                }
            )
            .AddHttpMessageHandler<RetryHandler>();
        services.AddScoped<GraphQLService>();
        return services;
    }
}
