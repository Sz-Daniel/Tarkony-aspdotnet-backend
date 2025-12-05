 using System.Collections.Generic;

public class ItemsRoot
{
    public ItemList Data { get; set; }
}

public class ItemList
{
    public List<ItemModel> Items { get; set; }
}

public class ItemModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public long? LastLowPrice { get; set; }
        public long? Low24HPrice { get; set; }
        public long? Avg24HPrice { get; set; }
        public long? High24HPrice { get; set; }
        public object ChangeLast48HPercent { get; set; }
        public object ChangeLast48H { get; set; }
        public long LastOfferCount { get; set; }
        public long Width { get; set; }
        public double Weight { get; set; }
        public bool HasGrid { get; set; }
        public string InspectImageLink { get; set; }
        public string BackgroundColor { get; set; }
        public string GridImageLink { get; set; }
        public string Description { get; set; }
        public string WikiLink { get; set; }
        public long Height { get; set; }
        public long? Velocity { get; set; }
        public object RecoilModifier { get; set; }
        public object Loudness { get; set; }
        public object AccuracyModifier { get; set; }
        public long? ErgonomicsModifier { get; set; }
        public DateTimeOffset Updated { get; set; }
        public List<object> BartersUsing { get; set; }
        public List<object> BartersFor { get; set; }
        public List<object> CraftsUsing { get; set; }
        public List<object> CraftsFor { get; set; }
        public List<Category> Categories { get; set; }
        public List<SellFor> SellFor { get; set; }
        public List<UsedInTask> UsedInTasks { get; set; }
        public List<ReceivedFromTask> ReceivedFromTasks { get; set; }
        public List<BuyFor> BuyFor { get; set; }
    }

    public  class BuyFor
    {
        public string Currency { get; set; }
        public long Price { get; set; }
        public long PriceRub { get; set; }
        public BuyForVendor Vendor { get; set; }
    }

    public  class BuyForVendor
    {
        public long? MinTraderLevel { get; set; }
        public long? BuyLimit { get; set; }
        public object TaskUnlock { get; set; }
        public Trader Trader { get; set; }
    }

    public  class Trader
    {
        public string Name { get; set; }
        public string ImageLink { get; set; }
        public List<Level> Levels { get; set; }
    }

    public  class Level
    {
        public long LevelLevel { get; set; }
        public long RequiredPlayerLevel { get; set; }
        public double RequiredReputation { get; set; }
        public long RequiredCommerce { get; set; }
    }

    public partial class Category
    {
        public string Name { get; set; }
    }

    public  class ReceivedFromTask
    {
        public string Name { get; set; }
        public FinishRewards FinishRewards { get; set; }
    }

    public  class FinishRewards
    {
        public List<FinishRewardsItem> Items { get; set; }
    }

    public  class FinishRewardsItem
    {
        public long Count { get; set; }
        public Category Item { get; set; }
    }

    public  class SellFor
    {
        public string Currency { get; set; }
        public long Price { get; set; }
        public long PriceRub { get; set; }
        public SellForVendor Vendor { get; set; }
    }

    public  class SellForVendor
    {
        public string Name { get; set; }
        public bool? FoundInRaidRequired { get; set; }
    }

    public  class UsedInTask
    {
        public string Name { get; set; }
        public List<Objective> Objectives { get; set; }
    }

    public  class Objective
    {
        public string Description { get; set; }
        public long? Count { get; set; }
        public Category Item { get; set; }
    }