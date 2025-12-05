using MongoExample.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using GraphQL;
using Item;
using ItemBase;
using System.Text.RegularExpressions;
using ItemDetail;
using Categories;
namespace Mongo.Services
{
        
    public class MongoDBService {

        private readonly ILogger<MongoDBService> _logger;
        private readonly GraphQLService _service;
        private readonly IMongoCollection<ItemModel> _itemsCollection;

        private readonly IMongoCollection<ItemBaseModel> _itemBaseCollection;

        private readonly IMongoCollection<ItemDetailModel> _itemDetailCollection;

        private readonly IMongoCollection<CategoryModel> _categoriesCollection;

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
            _itemBaseCollection = database.GetCollection<ItemBaseModel>("ItemBase");
            _itemDetailCollection = database.GetCollection<ItemDetailModel>("ItemDetail");
            _categoriesCollection = database.GetCollection<CategoryModel>("Categories");
            
        }
      
        public async Task<List<CategoryModel>> GetCategoriesAsync() { 
            return await _categoriesCollection.Find(new BsonDocument()).ToListAsync();
        }
        
        public async Task<List<ItemBaseModel>> GetItemBaseAsync() { 
            return await _itemBaseCollection.Find(new BsonDocument()).ToListAsync();
        }
        
        public async Task<List<ItemDetailModel>> GetItemDetailsAsync() { 
            return await _itemDetailCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<string> FetchItemDetailUploadAsync()
        {
            try
            {
                var itemBases = await _service.FetchItemDetailAsync();
                var models = new List<WriteModel<ItemDetailModel>>();

                foreach (var item in itemBases)
                {
                    var filter = Builders<ItemDetailModel>.Filter.Eq(x => x.Id, item.Id);
                    models.Add(new ReplaceOneModel<ItemDetailModel>(filter, item)
                    {
                        IsUpsert = true
                    });
                }

                var result = await _itemDetailCollection.BulkWriteAsync(models, new BulkWriteOptions { IsOrdered = false });


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
                _logger.LogError(ex, "ItemBase BulkWrite failed");
                throw;
            }
        }
    
        public async Task<string> FetchItemBaseUploadAsync()
        {
            try
            {
                var newItemBases = await _service.FetchItemBaseAsync();
                var models = new List<WriteModel<ItemBaseModel>>();

                foreach (var newItemBase in newItemBases)
                {
                    var filter = Builders<ItemBaseModel>.Filter.Eq(x => x.Id, newItemBase.Id);
                    models.Add(new ReplaceOneModel<ItemBaseModel>(filter, newItemBase)
                    {
                        IsUpsert = true
                    });
                }

                var result = await _itemBaseCollection.BulkWriteAsync(models, new BulkWriteOptions { IsOrdered = false });


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
                _logger.LogError(ex, "ItemBase BulkWrite failed");
                throw;
            }
        }
    
        public async Task<string> FetchCategoriesUploadAsync()
        {
            try
            {
                var newCategories = await _service.FetchCategoriesAsync();
                var models = new List<WriteModel<CategoryModel>>();

                foreach (var newCategory in newCategories)
                {
                    var filter = Builders<CategoryModel>.Filter.Eq(x => x.Id, newCategory.Id);
                    models.Add(new ReplaceOneModel<CategoryModel>(filter, newCategory)
                    {
                        IsUpsert = true
                    });
                }

                var result = await _categoriesCollection.BulkWriteAsync(models, new BulkWriteOptions { IsOrdered = false });


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
                _logger.LogError(ex, "Categories BulkWrite failed");
                throw;
            }
        }
        
}    
}
