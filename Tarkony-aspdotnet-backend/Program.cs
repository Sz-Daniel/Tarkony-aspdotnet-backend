var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGraphQL();

builder.Services.AddMongo(builder.Configuration);

builder.Services.AddQuartzJobs();

builder.Services.AddCorsPolicy();

//builder.Services.AddJWTAuth();

var app = builder.Build();

app.UseAppPipeline(app.Environment);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCustomMiddleware();

app.Run();
