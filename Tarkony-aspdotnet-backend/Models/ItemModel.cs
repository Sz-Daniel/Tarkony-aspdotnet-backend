/**
using System.Collections.Generic;

    public partial class SingleItemQueryType
    {
        public Data Data { get; set; }
    }

    public partial class Data
    {
        public List<DataItem> Items { get; set; }
    }

    public partial class DataItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public Uri WikiLink { get; set; }
        public List<object> CraftsUsing { get; set; }
        public List<object> CraftsFor { get; set; }
        public List<SellFor> SellFor { get; set; }
        public List<UsedInTask> UsedInTasks { get; set; }
        public List<BuyFor> BuyFor { get; set; }
        public List<ReceivedFromTask> ReceivedFromTasks { get; set; }
        public List<object> BartersUsing { get; set; }
        public List<object> BartersFor { get; set; }
    }

    public partial class BuyFor
    {
        public string Currency { get; set; }
        public long Price { get; set; }
        public long PriceRub { get; set; }
        public BuyForVendor Vendor { get; set; }
    }

    public partial class BuyForVendor
    {
        public long? MinTraderLevel { get; set; }
        public long? BuyLimit { get; set; }
        public object TaskUnlock { get; set; }
        public Trader Trader { get; set; }
        public string Name { get; set; }
    }

    public partial class Trader
    {
        public List<Level> Levels { get; set; }
    }

    public partial class Level
    {
        public long LevelLevel { get; set; }
        public long RequiredPlayerLevel { get; set; }
        public double RequiredReputation { get; set; }
        public long RequiredCommerce { get; set; }
    }

    public partial class ReceivedFromTask
    {
        public string Name { get; set; }
        public FinishRewards FinishRewards { get; set; }
    }

    public partial class FinishRewards
    {
        public List<FinishRewardsItem> Items { get; set; }
    }

    public partial class FinishRewardsItem
    {
        public long Count { get; set; }
        public ItemItem Item { get; set; }
    }

    public partial class ItemItem
    {
        public string Name { get; set; }
    }

    public partial class SellFor
    {
        public string Currency { get; set; }
        public long Price { get; set; }
        public long PriceRub { get; set; }
        public SellForVendor Vendor { get; set; }
    }

    public partial class SellForVendor
    {
        public string Name { get; set; }
        public bool? FoundInRaidRequired { get; set; }
    }

    public partial class UsedInTask
    {
        public string Name { get; set; }
        public List<Objective> Objectives { get; set; }
    }

    public partial class Objective
    {
        public string Description { get; set; }
        public long? Count { get; set; }
        public ItemItem Item { get; set; }
    }*/
    