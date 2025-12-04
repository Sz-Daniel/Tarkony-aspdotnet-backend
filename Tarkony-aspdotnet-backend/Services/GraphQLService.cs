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

    public async Task<List<Item>> FetchItemsAsync()
    {
        var request = new GraphQLRequest
        {
            Query = queries["ItemBaseQuery"]
        };

        var response = await _client.PostAsJsonAsync("", request);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"GraphQL HTTP error: {response.StatusCode}");

        var json = await response.Content.ReadFromJsonAsync<GraphQLResponse<ItemBaseModel>>();

        if (json?.Errors != null && json.Errors.Any())
            throw new Exception($"GraphQL errors: {string.Join(';', json.Errors.Select(e => e.Message))}");

        // Itt már csak a lista kell
        return json?.Data?.Items ?? new List<Item>();
    }

    public async Task<List<Categories>> FetchCategoriesAsync()
    {
        var request = new GraphQLRequest
        {
            Query = queries["CategoriesQuery"]
        };

        var response = await _client.PostAsJsonAsync("", request);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"GraphQL HTTP error: {response.StatusCode}");

        var json = await response.Content.ReadFromJsonAsync<GraphQLResponse<CategoriesModel>>();

        if (json?.Errors != null && json.Errors.Any())
            throw new Exception($"GraphQL errors: {string.Join(';', json.Errors.Select(e => e.Message))}");

        return json?.Data?.ItemCategories ?? new List<Categories>();
    }
    
    private static readonly Dictionary<string, string> queries = new()
    {
        { "ItemBaseQuery", @" query {
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
        }"},
        
        { "CategoriesQuery", @"query{
            itemCategories {
                id
                name
                normalizedName
                children { normalizedName }
                parent { normalizedName }
            }
        }"},

        {"StatusQuery",@"query {
            status {
                currentStatuses { message name status statusCode }
                generalStatus { message name status statusCode }
                messages { content solveTime statusCode time type }
            }
        }"},
        
        //Query = queries["ItemsQuery"];
    };
}

public class GraphQLResponse<TData>
{
    public TData? Data { get; set; }
    public List<GraphQLError>? Errors { get; set; }
}
public class GraphQLError
{
    public string Message { get; set; } = "";
}
