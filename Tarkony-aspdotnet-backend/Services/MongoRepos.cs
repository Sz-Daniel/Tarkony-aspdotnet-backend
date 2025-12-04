using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

public class ItemRepository
{
    private readonly IMongoCollection<BsonDocument> _items;

    public ItemRepository(IMongoClient client, IOptions<MongoDbSettings> settings)
    {
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _items = database.GetCollection<BsonDocument>("items_data");
    }

    public async Task<List<BsonDocument>> GetAllAsync() =>
        await _items.Find(_ => true).ToListAsync();

public async Task<BsonDocument?> GetByIdAsync(string id) =>
    await _items.Find(i => i["id"] == id).FirstOrDefaultAsync();
    public async Task CreateAsync(BsonDocument item) =>
        await _items.InsertOneAsync(item);

    public async Task UpdateAsync(string id, BsonDocument item) =>
        await _items.ReplaceOneAsync(i => i["id"]== id, item);

    public async Task DeleteAsync(string id) =>
        await _items.DeleteOneAsync(i => i["id"] == id);

            // Bulk insert JSON dokumentumok
    public async Task InsertBulkJsonAsync(IEnumerable<string> jsonDocs)
    {
        var docs = jsonDocs.Select(j => BsonDocument.Parse(j)).ToList();
        await _items.InsertManyAsync(docs);
    }
}
