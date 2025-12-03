using Microsoft.Extensions.Caching.Memory;

public interface IItemRepository
{
    System.Threading.Tasks.Task UpsertAsync(SingleItemResultType item);
    System.Threading.Tasks.Task<SingleItemResultType?> GetByIdAsync(string id);
}

public class MemoryItemRepository : IItemRepository
{
    private readonly IMemoryCache _cache;

    public MemoryItemRepository(IMemoryCache cache)
    {
        _cache = cache;
    }

    public System.Threading.Tasks.Task UpsertAsync(SingleItemResultType item)
    {
        string cacheKey = $"item-{item.Id}";
        _cache.Set(cacheKey, item, TimeSpan.FromMinutes(10));
        return System.Threading.Tasks.Task.CompletedTask;
    }

    public System.Threading.Tasks.Task<SingleItemResultType?> GetByIdAsync(string id)
    {
        string cacheKey = $"item-{id}";
        _cache.TryGetValue(cacheKey, out SingleItemResultType? item);
        return System.Threading.Tasks.Task.FromResult(item);
    }
}