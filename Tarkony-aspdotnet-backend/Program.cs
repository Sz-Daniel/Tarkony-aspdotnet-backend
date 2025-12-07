var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSeriLogLogging();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGraphQL();

builder.Services.AddMongo(builder.Configuration);

//builder.Services.AddQuartzJobs();

builder.Services.AddCorsPolicy();

//builder.Services.AddJWTAuth();

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
app.UseCors("AllowTarkonyFrontendOnly");
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.UseLoggerExtension(); // log + traceId
app.MapControllers();

app.Run();
