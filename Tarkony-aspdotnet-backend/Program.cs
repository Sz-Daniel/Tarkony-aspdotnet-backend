using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("GraphQLClient", client =>
{
    client.BaseAddress = new Uri("https://api.tarkov.dev/graphql");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
// In-memory caching for lightweight local usage
builder.Services.AddMemoryCache();

// GraphQL service wrapper registered for DI
builder.Services.AddScoped<GraphQLService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // részletes hibák fejlesztéskor
}
else
{
    app.UseExceptionHandler("/error"); // globális hiba kezelő endpoint
    app.UseHsts(); // biztonságos HTTPS header
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();

app.Use(async (context, next) =>
{
    Console.WriteLine($"[{context.Request.Method} {context.Request.Path} {DateTime.UtcNow}] Started.");
    await next(context);
    Console.WriteLine($"[{context.Request.Method} {context.Request.Path} {DateTime.UtcNow}] Finished.");
});

app.Run();

