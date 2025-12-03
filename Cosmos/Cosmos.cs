
using Microsoft.Azure.Cosmos;

public interface IItemRepository
{
    Task UpsertAsync(SingleItemResultType item);
    Task<SingleItemResultType?> GetByIdAsync(string id, string category);
}


public class CosmosItemRepository : IItemRepository
{
    private readonly Container _container;

    public CosmosItemRepository(CosmosClient client)
    {
        _container = client.GetContainer("TarkonyDb", "Items");
    }

    public async Task UpsertAsync(SingleItemQueryType item)
    {
        await _container.UpsertItemAsync(item, new PartitionKey(
            item.Categories.FirstOrDefault()?.NameValue ?? "Unknown"));
        
    }

    public async Task<SingleItemQueryType?> GetByIdAsync(string id, string category)
    {
        try
        {
            var response = await _container.ReadItemAsync<SingleItemQueryType>(
                id, new PartitionKey(category));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }
}

public class MemoryItemRepository : IItemRepository
{
    private readonly IMemoryCache _cache;

    public MemoryItemRepository(IMemoryCache cache)
    {
        _cache = cache;
    }

    public Task UpsertAsync(SingleItemQueryType item)
    {
        string cacheKey = $"item-{item.Id}";
        _cache.Set(cacheKey, item, TimeSpan.FromMinutes(10));
        return System.Threading.Tasks.Task.CompletedTask; // biztosan jó .NET 8-ban
    }

    public Task<SingleItemQueryType?> GetByIdAsync(string id, string category)
    {
        string cacheKey = $"item-{id}";
        _cache.TryGetValue(cacheKey, out SingleItemQueryType? item);
        return System.Threading.Tasks.Task.FromResult(item);
    }
}


