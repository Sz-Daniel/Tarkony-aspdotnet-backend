public class RetryHandler : DelegatingHandler
{
    private readonly ILogger<RetryHandler> _logger;
    private const int MaxRetries = 3;

    public RetryHandler(ILogger<RetryHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        int retryCount = 0;

        while (true)
        {
            try
            {
                // Lefuttatja a következő handler-t vagy magát a HttpClient-et
                return await base.SendAsync(request, cancellationToken);
            }
            catch (HttpRequestException ex) when (retryCount < MaxRetries)
            {
                retryCount++;
                _logger.LogWarning(
                    ex,
                    "HTTP request failed. Retrying {RetryCount}/{MaxRetries}...",
                    retryCount,
                    MaxRetries
                );

                // Exponential backoff: 1s, 2s, 4s
                var delay = TimeSpan.FromSeconds(Math.Pow(2, retryCount));
                await Task.Delay(delay, cancellationToken);
            }
        }
    }
}
