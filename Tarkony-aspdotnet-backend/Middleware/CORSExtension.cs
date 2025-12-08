public static class CORS
{
    public static IServiceCollection AddCorsPolicy(
        this IServiceCollection services,
        IWebHostEnvironment env
    )
    {
        services.AddCors(options =>
        {
            options.AddPolicy(
                "ProdCors",
                policy =>
                {
                    policy
                        .WithOrigins(
                            "https://tarkony-bygtfddsfgebe5df.westeurope-01.azurewebsites.net"
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                }
            );
        });

        return services;
    }
}
