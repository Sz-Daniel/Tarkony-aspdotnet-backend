using GraphQL;

public static class GraphQLExtension
{
    public static IServiceCollection AddGraphQL(this IServiceCollection services)
    {
        services.AddHttpClient(
            "GraphQLClient",
            client =>
            {
                client.BaseAddress = new Uri("https://api.tarkov.dev/graphql");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            }
        );
        services.AddScoped<GraphQLService>();
        return services;
    }
}
