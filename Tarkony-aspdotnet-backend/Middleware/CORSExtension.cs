public static class CORS
{
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(
                "AllowTarkonyFrontendOnly",
                policy =>
                {
                    policy
                        .WithOrigins(
                            "http://localhost:5173",
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
