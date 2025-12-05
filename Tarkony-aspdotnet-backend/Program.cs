
using MongoDB.Driver;
using Microsoft.Extensions.Options;

using MongoExample.Models;
using Mongo.Services;
using GraphQL;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("GraphQLClient", client =>
{
    client.BaseAddress = new Uri("https://api.tarkov.dev/graphql");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
builder.Services.AddScoped<GraphQLService>();

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddScoped<MongoDBService>();

builder.Services.AddQuartzHostedService(options =>
{
    options.WaitForJobsToComplete = true;
});
//add dependency injection for QuartzJobScheduler
builder.Services.AddSingleton<IQuartzJobScheduler, QuartzJobScheduler>();

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

