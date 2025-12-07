using GraphQL;
using MongoDB.Driver;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger
    )
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext ctx)
    {
        try
        {
            await _next(ctx);
        }
        catch (GraphQLException ex)
        {
            _logger.LogError(ex, "GraphQL hiba");
            await WriteProblem(
                ctx,
                StatusCodes.Status502BadGateway,
                "graphql_error",
                "GraphQL query failed. Please try again later."
            );
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HttpRequest error");
            await WriteProblem(
                ctx,
                StatusCodes.Status503ServiceUnavailable,
                "HttpRequest error",
                "HttpRequest error. Please try again later."
            );
        }
        catch (MongoException ex)
        {
            _logger.LogError(ex, "MongoDB error");
            await WriteProblem(
                ctx,
                StatusCodes.Status503ServiceUnavailable,
                "database_error",
                "Database operation failed. Please try again later."
            );
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogInformation(ex, "Request cancelled");
            await WriteProblem(ctx, 499, "cancelled", "Request was cancelled.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled server error");
            await WriteProblem(
                ctx,
                StatusCodes.Status500InternalServerError,
                "server_error",
                "Unexpected error occurred. Contact support with traceId."
            );
        }
    }

    private static async Task WriteProblem(
        HttpContext ctx,
        int status,
        string type,
        string message,
        object? details = null
    )
    {
        ctx.Response.StatusCode = status;
        ctx.Response.ContentType = "application/json";
        var traceId = ctx.TraceIdentifier;

        var payload = new
        {
            type,
            message,
            traceId,
            details
        };

        await ctx.Response.WriteAsJsonAsync(payload);
    }
}
