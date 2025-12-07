public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseAppPipeline(
        this IApplicationBuilder app,
        IWebHostEnvironment env
    )
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseExceptionHandler("/error");
            app.UseHsts();
        }

        app.UseCors("AllowTarkonyFrontendOnly");
        app.UseRouting();

        return app;
    }
}
