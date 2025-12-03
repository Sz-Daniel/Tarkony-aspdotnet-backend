public class GraphQLRequest
{
    public string Query { get; set; } = string.Empty;
    public object? Variables { get; set; }
}

public class GraphQLService
{
    private readonly HttpClient _client;

    public GraphQLService(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("GraphQLClient");
    }

    public async Task<List<ItemBaseQueryType>> FetchItemsAsync()
    {
        var request = new GraphQLRequest
        {
            Query = @"
              query {
                items(limit: 2) {
                  id
                  name
                  category { normalizedName }
                  gridImageLink
                  changeLast48h
                  changeLast48hPercent
                  sellFor { priceRUB vendor { name } }
                  buyFor { priceRUB vendor { name } }
                }
              }"
        };

        var response = await _client.PostAsJsonAsync("", request);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"GraphQL HTTP error: {response.StatusCode}");

        var json = await response.Content.ReadFromJsonAsync<GraphQLResponse>();

        if (json?.Errors != null && json.Errors.Any())
            throw new Exception($"GraphQL errors: {string.Join(';', json.Errors.Select(e => e.Message))}");

        return json?.Data?.Items ?? new List<ItemBaseQueryType>();
    }
}

public class GraphQLResponse
{
    public GraphQLData? Data { get; set; }
    public List<GraphQLError>? Errors { get; set; }
}

public class GraphQLData
{
    public List<ItemBaseQueryType> Items { get; set; } = new();
}

public class GraphQLError
{
    public string Message { get; set; } = "";
}

