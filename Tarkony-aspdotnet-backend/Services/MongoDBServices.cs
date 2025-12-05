using MongoExample.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using GraphQL;
namespace Mongo.Services
{
        
    public class MongoDBService {

        private readonly ILogger<MongoDBService> _logger;
        private readonly GraphQLService _service;
        private readonly IMongoCollection<ItemModel> _itemsCollection;

        public MongoDBService(
            IOptions<MongoDBSettings> mongoDBSettings,
            ILogger<MongoDBService> logger,
            GraphQLService service)
        {
        _logger = logger;
        _service = service;
            
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _itemsCollection = database.GetCollection<ItemModel>("items_data");
        }

        public async Task<string> FetchItemUploadAsync()
        {
            try
            {
                var newItems = await _service.FetchItemsAsync();
                var models = new List<WriteModel<ItemModel>>();

                foreach (var newItem in newItems)
                {
                    var filter = Builders<ItemModel>.Filter.Eq(x => x.Id, newItem.Id);
                    models.Add(new ReplaceOneModel<ItemModel>(filter, newItem)
                    {
                        IsUpsert = true
                    });
                }

                var result = await _itemsCollection.BulkWriteAsync(models, new BulkWriteOptions { IsOrdered = false });


                 _logger.LogInformation("IsAcknowledged: {result.IsAcknowledged}",result.IsAcknowledged);
                 _logger.LogInformation("MatchedCount: {result.MatchedCount}",result.MatchedCount);
                 _logger.LogInformation("ModifiedCount: {result.ModifiedCount}",result.ModifiedCount);
                 _logger.LogInformation("InsertedCount: {result.InsertedCount}",result.InsertedCount);
                 _logger.LogInformation("DeletedCount: {result.DeletedCount}",result.DeletedCount);

                var dto = new
                {
                    IsAcknowledged = result.IsAcknowledged,
                    MatchedCount = result.MatchedCount,
                    ModifiedCount = result.ModifiedCount,
                    InsertedCount = result.InsertedCount,
                    DeletedCount = result.DeletedCount,
                    Upserts = result.Upserts.Select(u => new { u.Index, u.Id }).ToList()
                };



                return System.Text.Json.JsonSerializer.Serialize(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BulkWrite failed");
                throw;
            }
        }

        public async Task<List<ItemModel>> GetAllItemsAsync() { 
            return await _itemsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<string> DeleteAllAsync() {
            try
            {
                var result = await _itemsCollection.DeleteManyAsync(FilterDefinition<ItemModel>.Empty);
                _logger.LogInformation("IsAcknowledged: {result.IsAcknowledged}",result.IsAcknowledged);
                _logger.LogInformation("DeletedCount: {result.DeletedCount}",result.DeletedCount);

                var dto = new
                {
                    IsAcknowledged = result.IsAcknowledged,
                    DeletedCount = result.DeletedCount,
                };
                return System.Text.Json.JsonSerializer.Serialize(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteAll failed");
                throw;
            }
        }


    }
}
/*

        public async Task<List<Playlist>> GetAsync() { 
            return await _playlistCollection.Find(new BsonDocument()).ToListAsync();
        }
        public async Task CreateAsync(Playlist playlist) { 
            await _playlistCollection.InsertOneAsync(playlist);
            return;
        }
        public async Task AddToPlaylistAsync(string id, string movieId) {
            FilterDefinition<Playlist> filter = Builders<Playlist>.Filter.Eq("Id", id);
            UpdateDefinition<Playlist> update = Builders<Playlist>.Update.AddToSet<string>("movieIds", movieId);
            await _playlistCollection.UpdateOneAsync(filter, update);
            return;
        }
        public async Task DeleteAsync(string id) {
            FilterDefinition<Playlist> filter = Builders<Playlist>.Filter.Eq("Id", id);
            await _playlistCollection.DeleteOneAsync(filter);
            return;
        }
*/