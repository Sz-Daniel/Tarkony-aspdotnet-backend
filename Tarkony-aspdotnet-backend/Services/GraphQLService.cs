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

        public GraphQLService(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("GraphQLClient");
        }

        public async Task<List<CategoryModel>> FetchAPIStatusAsync()
        {
            var request = new GraphQLRequest { Query = queries["APIStatusQuery"] };

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
            var request = new GraphQLRequest { Query = queries["CategoriesQuery"] };

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
            var request = new GraphQLRequest { Query = queries["ItemsQuery"] };

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

        private static readonly Dictionary<string, string> queries =
            new()
            {
                //Query = queries["ItemsQuery"];
                {
                    "APIStatusQuery",
                    @"query {
        status {
        currentStatuses { message name status statusCode }
        generalStatus { message name status statusCode }
        messages { content solveTime statusCode time type }
        }
        }"
                },
                {
                    "CategoriesQuery",
                    @"query{
        itemCategories {
        id
        name
        normalizedName
        children { normalizedName }
        parent { normalizedName }
        }
        }"
                },
                {
                    "ItemsQuery",
                    @" query {
            items {
                id
                name
                category {
                normalizedName
                }
                gridImageLink
                changeLast48h
                changeLast48hPercent
                normalizedName
                wikiLink
                sellFor {
                currency
                price
                priceRUB
                vendor {
                    name
                    ... on FleaMarket {
                    foundInRaidRequired
                    }
                }
                }
                buyFor {
                currency
                price
                priceRUB
                vendor {
                    name
                    ... on TraderOffer {
                    minTraderLevel
                    buyLimit
                    trader {
                        name
                        imageLink
                        levels {
                        level
                        requiredPlayerLevel
                        requiredReputation
                        requiredCommerce
                        }
                    }
                    taskUnlock {
                        name
                        minPlayerLevel
                    }
                    }
                }
                }
                bartersUsing {
                id
                level
                buyLimit
                taskUnlock {
                    name
                    minPlayerLevel
                }
                trader {
                    name
                    imageLink
                    levels {
                    level
                    requiredPlayerLevel
                    requiredReputation
                    requiredCommerce
                    }
                }
                rewardItems {
                    count
                    item {
                    id
                    gridImageLink
                    name
                    }
                }
                requiredItems {
                    count
                    item {
                    id
                    gridImageLink
                    name
                    }
                }
                }
                bartersFor {
                id
                level
                buyLimit
                taskUnlock {
                    name
                    minPlayerLevel
                }
                trader {
                    name
                    imageLink
                    levels {
                    level
                    requiredPlayerLevel
                    requiredReputation
                    requiredCommerce
                    }
                }
                rewardItems {
                    count
                    item {
                    id
                    gridImageLink
                    name
                    }
                }
                requiredItems {
                    count
                    item {
                    id
                    gridImageLink
                    name
                    }
                }
                }
                craftsUsing {
                id
                duration
                level
                station {
                    name
                    imageLink
                }
                taskUnlock {
                    name
                    minPlayerLevel
                }
                rewardItems {
                    count
                    item {
                    id
                    gridImageLink
                    name
                    }
                }
                requiredItems {
                    count
                    item {
                    id
                    gridImageLink
                    name
                    }
                }
                }
                craftsFor {
                id
                duration
                level
                station {
                    name
                    imageLink
                }
                taskUnlock {
                    name
                    minPlayerLevel
                }
                rewardItems {
                    count
                    item {
                    id
                    gridImageLink
                    name
                    }
                }
                requiredItems {
                    count
                    item {
                    id
                    gridImageLink
                    name
                    }
                }
                }
                usedInTasks {
                name
                objectives {
                    ... on TaskObjectiveItem {
                    description
                    count
                    item {
                        name
                    }
                    }
                }
                }
                receivedFromTasks {
                name
                finishRewards {
                    items {
                    count
                    item {
                        name
                    }
                    }
                }
                }
            }
            }"
                },
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
}
