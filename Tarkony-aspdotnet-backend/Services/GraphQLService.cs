using Categories;
using Items.External;

namespace GraphQL
{
    public class GraphQLRequest
    {
        public string Query { get; set; } = string.Empty;
        public object? Variables { get; set; }
    }

    public class GraphQLService
    {
        private readonly HttpClient _client;
        private readonly ILogger<GraphQLService> _logger;

        public GraphQLService(IHttpClientFactory factory, ILogger<GraphQLService> logger)
        {
            _client = factory.CreateClient("GraphQLClient");
            _logger = logger;
        }

        public async Task<List<CategoryModel>> FetchAPIStatusAsync()
        {
            var request = new GraphQLRequest { Query = GraphQL.Query.queries["APIStatusQuery"] };

            var response = await _client.PostAsJsonAsync("", request);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"GraphQL HTTP error: {response.StatusCode}");

            var json = await response.Content.ReadFromJsonAsync<GraphQLResponse<CategoryList>>();

            if (json?.Errors != null && json.Errors.Any())
                throw new Exception(
                    $"GraphQL errors: {string.Join(';', json.Errors.Select(e => e.Message))}"
                );

            return json?.Data?.ItemCategories ?? new List<CategoryModel>();
        }

        public async Task<List<CategoryModel>> FetchCategoriesAsync()
        {
            var request = new GraphQLRequest { Query = GraphQL.Query.queries["CategoriesQuery"] };

            var response = await _client.PostAsJsonAsync("", request);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"GraphQL HTTP error: {response.StatusCode}");

            var json = await response.Content.ReadFromJsonAsync<GraphQLResponse<CategoryList>>();

            if (json?.Errors != null && json.Errors.Any())
                throw new Exception(
                    $"GraphQL errors: {string.Join(';', json.Errors.Select(e => e.Message))}"
                );

            return json?.Data?.ItemCategories ?? new List<CategoryModel>();
        }

        public async Task<List<ItemsModel>> FetchItemsAsync()
        {
            var request = new GraphQLRequest { Query = GraphQL.Query.queries["ItemsQuery"] };

            var response = await _client.PostAsJsonAsync("", request);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"GraphQL HTTP error: {response.StatusCode}");

            var json = await response.Content.ReadFromJsonAsync<GraphQLResponse<ItemsList>>();

            if (json?.Errors != null && json.Errors.Any())
                throw new Exception(
                    $"GraphQL errors: {string.Join(';', json.Errors.Select(e => e.Message))}"
                );

            return json?.Data?.Items ?? new List<ItemsModel>();
        }
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
}
