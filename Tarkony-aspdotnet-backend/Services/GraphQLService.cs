using Categories;
using Item;
using ItemBase;
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

   public async Task<List<ItemBaseModel>> FetchItemDetailAsync()
    {
        var request = new GraphQLRequest
        {
            Query = queries["ItemBaseQuery"]
        };

        var response = await _client.PostAsJsonAsync("", request);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"GraphQL HTTP error: {response.StatusCode}");

        var json = await response.Content.ReadFromJsonAsync<GraphQLResponse<ItemBaseList>>();

        if (json?.Errors != null && json.Errors.Any())
            throw new Exception($"GraphQL errors: {string.Join(';', json.Errors.Select(e => e.Message))}");

        // Itt már csak a lista kell
        return json?.Data?.Items ?? new List<ItemBaseModel>();
    }

    public async Task<List<ItemBaseModel>> FetchItemBaseAsync()
    {
        var request = new GraphQLRequest
        {
            Query = queries["ItemBaseQuery"]
        };

        var response = await _client.PostAsJsonAsync("", request);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"GraphQL HTTP error: {response.StatusCode}");

        var json = await response.Content.ReadFromJsonAsync<GraphQLResponse<ItemBaseList>>();

        if (json?.Errors != null && json.Errors.Any())
            throw new Exception($"GraphQL errors: {string.Join(';', json.Errors.Select(e => e.Message))}");

        // Itt már csak a lista kell
        return json?.Data?.Items ?? new List<ItemBaseModel>();
    }


    public async Task<List<CategoryModel>> FetchCategoriesAsync()
    {
        var request = new GraphQLRequest
        {
            Query = queries["CategoriesQuery"]
        };

        var response = await _client.PostAsJsonAsync("", request);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"GraphQL HTTP error: {response.StatusCode}");

        var json = await response.Content.ReadFromJsonAsync<GraphQLResponse<CategoryList>>();

        if (json?.Errors != null && json.Errors.Any())
            throw new Exception($"GraphQL errors: {string.Join(';', json.Errors.Select(e => e.Message))}");

        return json?.Data?.ItemCategories ?? new List<CategoryModel>();
    }
    
    private static readonly Dictionary<string, string> queries = new()
    {
      { "StatusQuery",@"query {
      status {
      currentStatuses { message name status statusCode }
      generalStatus { message name status statusCode }
      messages { content solveTime statusCode time type }
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
      { "ItemDetailQuery", @" query {
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


      { "ItemsQuery", @" query {
      items(limit: 2) {

      id
      name
      shortName
      categories { name }
      lastLowPrice
      low24hPrice
      avg24hPrice
      high24hPrice
      changeLast48hPercent
      changeLast48h
      lastOfferCount
      width
      weight
      hasGrid
      inspectImageLink
      backgroundColor
      gridImageLink
      description
      wikiLink
      height
      velocity
      recoilModifier
      loudness
      accuracyModifier
      ergonomicsModifier
      updated
      sellFor { currency price priceRUB
      vendor { name
      ... on FleaMarket { foundInRaidRequired }
      }
      }

      buyFor { currency price priceRUB
      vendor {
      ... on TraderOffer { minTraderLevel buyLimit
      trader { name imageLink
      levels { level requiredPlayerLevel requiredReputation requiredCommerce }
      }
      taskUnlock { name minPlayerLevel }
      }
      }
      }

      bartersUsing { id level buyLimit
      taskUnlock { name minPlayerLevel
      }
      trader { name imageLink
      levels { level requiredPlayerLevel requiredReputation requiredCommerce }
      }
      rewardItems { count
      item { id gridImageLink name }
      }
      requiredItems { count
      item { id gridImageLink name }
      }
      }  

      bartersFor { id level buyLimit
      taskUnlock { name minPlayerLevel }
      trader { name imageLink
      levels { level requiredPlayerLevel requiredReputation requiredCommerce }
      }
      rewardItems { count
      item { id gridImageLink name }
      }
      requiredItems { count
      item { id gridImageLink name}
      }
      } 

      craftsUsing { id duration level
      station { name imageLink }
      taskUnlock { name minPlayerLevel }
      rewardItems { count
      item { id gridImageLink name }
      }
      requiredItems { count
      item { id gridImageLink name }
      }
      }  

      craftsFor { id duration level
      station { name imageLink }
      taskUnlock { name minPlayerLevel }
      rewardItems { count
      item { id gridImageLink name }
      }
      requiredItems { count
      item { id gridImageLink name }
      }
      }

      usedInTasks { name
      objectives {
      ... on TaskObjectiveItem { description count
      item { name }
      }
      }
      }
      receivedFromTasks { name
      finishRewards {
      items { count
      item { name }
      }
      }
      }
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

}


/**
    public async Task<List<ItemModel>> FetchItemsAsync()
    {
        var request = new GraphQLRequest
        {
            Query = queries["ItemsQuery"]
        };
        var response = await _client.PostAsJsonAsync("", request);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"GraphQL HTTP error: {response.StatusCode}");

        var json = await response.Content.ReadFromJsonAsync<GraphQLResponse<ItemList>>();

        if (json?.Errors != null && json.Errors.Any())
            throw new Exception($"GraphQL errors: {string.Join(';', json.Errors.Select(e => e.Message))}");

        return json?.Data.Items ?? new List<ItemModel>();
    }

*/