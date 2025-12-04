
using MongoDB.Driver;
using Microsoft.Extensions.Options;


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

// Bind settings
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

// Register MongoClient as singleton
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

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

