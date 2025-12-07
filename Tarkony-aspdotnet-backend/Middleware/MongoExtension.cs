using Mongo.Services;
using MongoExample.Models;

public static class MongoExtensions
{
    public static IServiceCollection AddMongo(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        services.Configure<MongoDBSettings>(config.GetSection("MongoDB"));
        services.AddScoped<MongoDBService>();
        return services;
    }
}
