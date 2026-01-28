var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSeriLogLogging();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRateLimiterExtension();

builder.Services.AddGraphQL(builder.Configuration);

builder.Services.AddMongo(builder.Configuration);

//builder.Services.AddQuartzJobs();

builder.Services.AddCorsPolicy(builder.Environment, builder.Configuration);

//builder.Services.AddJWTAuth();
builder.Services.AddHealthChecks();
var app = builder.Build();

//app.UseAppPipeline(app.Environment);
app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    //app.UseExceptionHandler();
    app.UseHsts();
}

app.UseRouting();
if (builder.Environment.IsDevelopment())
{
    app.UseCors("DevCors");
}
else
{
    app.UseCors("ProdCors");
}

app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.UseLoggerExtension(); // log + traceId
app.MapControllers();
app.MapHealthChecks("/health");
app.MapGet("/", () => "API is running");

app.Run();
