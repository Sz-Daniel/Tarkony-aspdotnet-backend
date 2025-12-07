using Mongo.Services;
using MongoExample.Models;

public static class MongoExtensions
{
    public static IServiceCollection AddMongo(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        services
            .AddOptions<MongoDBSettings>()
            .Bind(config.GetSection("MongoDB"))
            .ValidateDataAnnotations();
        services.AddScoped<MongoDBService>();
        return services;
    }
}
