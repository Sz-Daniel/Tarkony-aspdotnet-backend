public static class CORS
{
    public static IServiceCollection AddCorsPolicy(
        this IServiceCollection services,
        IWebHostEnvironment env,
        IConfiguration config
    )
    {
        var frontend = config["FRONTEND"];
        Console.WriteLine($"ENV: {env.EnvironmentName} FRONTEND: {frontend}");
        if (env.IsDevelopment())
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "DevCors",
                    policy =>
                    {
                        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    }
                );
            });
        }
        else
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "ProdCors",
                    policy =>
                    {
                        policy.WithOrigins(frontend).AllowAnyHeader().AllowAnyMethod();
                    }
                );
                options.AddPolicy(
                    "PublicAPI",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    }
                );
            });
        }

        return services;
    }
}
