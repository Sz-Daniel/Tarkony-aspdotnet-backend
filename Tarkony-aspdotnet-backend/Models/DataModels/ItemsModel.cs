namespace Items
{
    using System.Collections.Generic;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    namespace Domain
    {
        public class Items
        {
            public string id { get; set; }
            public string name { get; set; }
            public string iconURL { get; set; }
            public PriceDeal? bestSeller { get; set; }
            public PriceDeal? bestBuy { get; set; }
            public double changePrice { get; set; }
            public double changePercent { get; set; }
            public string category { get; set; }
            public string normalizedName { get; set; }
            public string wiki { get; set; }
            public List<HistoricalPrices> historicalPrices { get; set; }
            public List<SellTo> sellTo { get; set; }
            public List<BuyFrom> buyFrom { get; set; }
            public List<Barter> barterInput { get; set; }
            public List<Barter> barterOutput { get; set; }
            public List<Craft> craftInput { get; set; }
            public List<Craft> craftOutput { get; set; }
            public List<TaskNeed>? taskNeed { get; set; }
            public List<TaskGive>? taskGive { get; set; }
        }

        public class HistoricalPrices
        {
            public int offerCount { get; set; }
            public int offerCountMin { get; set; }
            public int price { get; set; }
            public int priceMin { get; set; }
            public string timestamp { get; set; }
        }

        public class TaskGive
        {
            public string name { get; set; }
            public List<TaskItem>? reward { get; set; }
        }

        public class TaskNeed
        {
            public string name { get; set; }
            public List<Task>? task { get; set; }
        }

        public class Task
        {
            public string description { get; set; }
            public string name { get; set; }
            public int count { get; set; }
        }

        public class TaskItem
        {
            public string name { get; set; }
            public int count { get; set; }
        }

        public class PriceDeal
        {
            public int price { get; set; }
            public string place { get; set; }
        }

        public class Craft : CraftRequirement
        {
            public List<ResponseCountedItem> inputItems { get; set; }
            public List<ResponseCountedItem> outputItems { get; set; }
        }

        public class CraftRequirement
        {
            public string id { get; set; }
            public int duration { get; set; }
            public StationRequirement? stationRequirement { get; set; }
            public QuestRequirement? questRequirement { get; set; }
        }

        public class StationRequirement
        {
            public int level { get; set; }
            public string stationName { get; set; }
            public string stationIcon { get; set; }
        }

        public class Barter : PurchaseRequirement
        {
            public List<ResponseCountedItem> inputItems { get; set; }
            public List<ResponseCountedItem> outputItems { get; set; }
        }

        public class ResponseCountedItem
        {
            public int count { get; set; }
            public string id { get; set; }
            public string img { get; set; }
            public string name { get; set; }
        }

        public class SellTo : PriceInfo
        {
            public bool fir { get; set; }
            public string traderName { get; set; }
        }

        public class BuyFrom
        {
            public int priceRub { get; set; }
            public int price { get; set; }
            public string priceCurrency { get; set; }
            public string id { get; set; }
            public int limit { get; set; }
            public PlayertoTraderRequirements? playertoTraderRequirements { get; set; }
            public QuestRequirement? questRequirement { get; set; }
        }

        public class PriceInfo
        {
            public int priceRub { get; set; }
            public int price { get; set; }
            public string priceCurrency { get; set; }
        }

        public class PurchaseRequirement
        {
            public string id { get; set; }
            public int limit { get; set; }
            public PlayertoTraderRequirements PlayertoTraderRequirements { get; set; }
            public QuestRequirement QuestRequirement { get; set; }
        }

        public class QuestRequirement
        {
            public int level { get; set; }
            public string name { get; set; }
        }

        public class PlayertoTraderRequirements
        {
            public string traderName { get; set; }
            public string traderIcon { get; set; }
            public int traderLevel { get; set; }
            public int playerLevel { get; set; }
            public double reputation { get; set; }
            public int commerce { get; set; }
        }
    }

    namespace External
    {
        public class ItemsRoot
        {
            public ItemsList Data { get; set; }
        }

        public class ItemsList
        {
            public List<ItemsModel> Items { get; set; }
        }

        public class ItemsModel
        {
            public string Id { get; set; }

            [BsonRepresentation(BsonType.String)]
            public string Name { get; set; }
            public string GridImageLink { get; set; }
            public double? ChangeLast48h { get; set; }
            public double? ChangeLast48hPercent { get; set; }

            [BsonRepresentation(BsonType.String)]
            public string NormalizedName { get; set; }

            [BsonRepresentation(BsonType.String)]
            public string WikiLink { get; set; }
            public Category Category { get; set; }
            public List<HistoricalPrices> HistoricalPrices { get; set; }
            public List<Sell> SellFor { get; set; }
            public List<Buy> BuyFor { get; set; }
            public List<Barter> BartersUsing { get; set; }
            public List<Barter> BartersFor { get; set; }
            public List<Craft> CraftsUsing { get; set; }
            public List<Craft> CraftsFor { get; set; }
            public List<Task> UsedInTasks { get; set; }
        }

        public class HistoricalPrices
        {
            public int OfferCount { get; set; }
            public int OfferCountMin { get; set; }
            public int Price { get; set; }
            public int PriceMin { get; set; }
            public string Timestamp { get; set; }
        }

        public class Category
        {
            public string NormalizedName { get; set; }
        }

        public class Sell
        {
            public string Currency { get; set; }
            public double? Price { get; set; }
            public double? PriceRUB { get; set; }
            public Vendor Vendor { get; set; }
        }

        public class Vendor
        {
            public bool? FoundInRaidRequired { get; set; }
            public string Name { get; set; }
        }

        public class Buy
        {
            public string Currency { get; set; }
            public double? Price { get; set; }
            public double? PriceRUB { get; set; }
            public Vendor Vendor { get; set; }
            public TraderInfo Trader { get; set; }
            public TaskUnlock TaskUnlock { get; set; }
        }

        public class TraderInfo
        {
            public string Name { get; set; }
            public string ImageLink { get; set; }
            public List<TraderLevel> Levels { get; set; }
        }

        public class TraderLevel
        {
            public int Level { get; set; }
            public int RequiredPlayerLevel { get; set; }
            public double RequiredReputation { get; set; }
            public int RequiredCommerce { get; set; }
        }

        public class TaskUnlock
        {
            public string Name { get; set; }
            public int? MinPlayerLevel { get; set; }
        }

        public class Barter
        {
            public string Id { get; set; }
            public int Level { get; set; }
            public int? BuyLimit { get; set; }
            public TaskUnlock TaskUnlock { get; set; }
            public TraderInfo Trader { get; set; }
            public List<RewardItem> RewardItems { get; set; }
            public List<RequiredItem> RequiredItems { get; set; }
        }

        public class RewardItem
        {
            public int? Count { get; set; }
            public Item Item { get; set; }
        }

        public class RequiredItem
        {
            public double Count { get; set; }
            public Item Item { get; set; }
        }

        public class Craft
        {
            public string Id { get; set; }
            public int Duration { get; set; }
            public int Level { get; set; }
            public Station Station { get; set; }
            public TaskUnlock TaskUnlock { get; set; }
            public List<RewardItem> RewardItems { get; set; }
            public List<RequiredItem> RequiredItems { get; set; }
        }

        public class Station
        {
            public string Name { get; set; }
            public string ImageLink { get; set; }
        }

        public class Task
        {
            public string Name { get; set; }
            public List<Objective> Objectives { get; set; }
        }

        public class Objective
        {
            public string Description { get; set; }
            public int Count { get; set; }
            public Item Item { get; set; }
        }

        public class ReceivedFromTasksItem
        {
            public string Name { get; set; }
            public FinishRewards FinishRewards { get; set; }
        }

        public class FinishRewards
        {
            public List<FinishRewardsItem> Items { get; set; }
        }

        public class FinishRewardsItem
        {
            public int Count { get; set; }
            public Item Item { get; set; }
        }

        public class Item
        {
            public string Name { get; set; }
        }
    }
}
