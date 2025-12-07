using Categories;
using GraphQL;
using Items.Adapter;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoExample.Models;

namespace Mongo.Services
{
    public class MongoDBService
    {
        private readonly ILogger<MongoDBService> _logger;
        private readonly GraphQLService _service;
        private readonly IMongoCollection<Items.Domain.Items> _itemsCollection;
        private readonly IMongoCollection<CategoryModel> _categoriesCollection;

        public MongoDBService(
            IOptions<MongoDBSettings> mongoDBSettings,
            ILogger<MongoDBService> logger,
            GraphQLService service
        )
        {
            _logger = logger;
            _service = service;

            const string itemsCollection = "Items";
            const string categoriesCollection = "Categories";

            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);

            _itemsCollection = database.GetCollection<Items.Domain.Items>(itemsCollection);
            _categoriesCollection = database.GetCollection<CategoryModel>(categoriesCollection);
        }

        public async Task<List<CategoryModel>> GetCategoriesAsync()
        {
            return await _categoriesCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<List<Contracts.ItemBase.Items>> GetItemBaseAsync()
        {
            var result = await _itemsCollection
                .Find(FilterDefinition<Items.Domain.Items>.Empty)
                // .Limit(10)
                // .Skip(10)
                .Project(
                    x =>
                        new Contracts.ItemBase.Items
                        {
                            id = x.id,
                            name = x.name,
                            iconURL = x.iconURL,
                            bestSeller = x.bestSeller,
                            bestBuy = x.bestBuy,
                            changePrice = x.changePrice,
                            changePercent = x.changePercent,
                            category = x.category,
                        }
                )
                .ToListAsync();
            return result;
        }

        public async Task<Contracts.ItemDetail.Items> GetItemDetailAsync(string _id)
        {
            var result = await _itemsCollection
                .Find(x => x.id == _id)
                .Limit(1)
                //.Skip(10)
                .Project(
                    x =>
                        new Contracts.ItemDetail.Items
                        {
                            id = x.id,
                            name = x.name,
                            normalizedName = x.normalizedName,
                            wiki = x.wiki,
                            sellTo = x.sellTo,
                            buyFrom = x.buyFrom,
                            barterInput = x.barterInput,
                            barterOutput = x.barterOutput,
                            craftInput = x.craftInput,
                            craftOutput = x.craftOutput,
                            taskNeed = x.taskNeed,
                            taskGive = x.taskGive,
                        }
                )
                .FirstOrDefaultAsync();
            return result;
        }

        //Test
        public async Task<List<Contracts.ItemBase.Items>> GetSearcBarAsync(string word)
        {
            var result = await _itemsCollection
                .Find(FilterDefinition<Items.Domain.Items>.Empty)
                // .Limit(10)
                // .Skip(10)
                .Project(
                    x =>
                        new Contracts.ItemBase.Items
                        {
                            id = x.id,
                            name = x.name,
                            iconURL = x.iconURL,
                            bestSeller = x.bestSeller,
                            bestBuy = x.bestBuy,
                            changePrice = x.changePrice,
                            changePercent = x.changePercent,
                            category = x.category,
                        }
                )
                .ToListAsync();
            return result;
        }

        public async Task<List<BsonDocument>> GetSearcByNameAsync(string word)
        {
            var result = await _itemsCollection
                .Find(x => x.name == word)
                .Project(new BsonDocument())
                .ToListAsync();
            return result;
        }

        //Upload to the database
        public async Task<string> FetchCategoriesUploadAsync()
        {
            var newCategories = await _service.FetchCategoriesAsync();
            var models = new List<WriteModel<CategoryModel>>();

            foreach (var newCategory in newCategories)
            {
                var filter = Builders<CategoryModel>.Filter.Eq(x => x.Id, newCategory.Id);
                models.Add(
                    new ReplaceOneModel<CategoryModel>(filter, newCategory) { IsUpsert = true }
                );
            }

            var result = await _categoriesCollection.BulkWriteAsync(
                models,
                new BulkWriteOptions { IsOrdered = false }
            );

            _logger.LogInformation(
                "IsAcknowledged: {result.IsAcknowledged}",
                result.IsAcknowledged
            );
            _logger.LogInformation("MatchedCount: {result.MatchedCount}", result.MatchedCount);
            _logger.LogInformation("ModifiedCount: {result.ModifiedCount}", result.ModifiedCount);
            _logger.LogInformation("InsertedCount: {result.InsertedCount}", result.InsertedCount);
            _logger.LogInformation("DeletedCount: {result.DeletedCount}", result.DeletedCount);

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

        public async Task<string> FetchItemsUploadAsync()
        {
            var externalItems = await _service.FetchItemsAsync();
            var domainItems = ItemsAdapter.ToDomain(externalItems);
            var models = new List<WriteModel<Items.Domain.Items>>();

            foreach (var item in domainItems)
            {
                var filter = Builders<Items.Domain.Items>.Filter.Eq(x => x.id, item.id);
                models.Add(
                    new ReplaceOneModel<Items.Domain.Items>(filter, item) { IsUpsert = true }
                );
            }

            var result = await _itemsCollection.BulkWriteAsync(
                models,
                new BulkWriteOptions { IsOrdered = false }
            );

            _logger.LogInformation(
                "IsAcknowledged: {result.IsAcknowledged}",
                result.IsAcknowledged
            );
            _logger.LogInformation("MatchedCount: {result.MatchedCount}", result.MatchedCount);
            _logger.LogInformation("ModifiedCount: {result.ModifiedCount}", result.ModifiedCount);
            _logger.LogInformation("InsertedCount: {result.InsertedCount}", result.InsertedCount);
            _logger.LogInformation("DeletedCount: {result.DeletedCount}", result.DeletedCount);

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
    }
}
