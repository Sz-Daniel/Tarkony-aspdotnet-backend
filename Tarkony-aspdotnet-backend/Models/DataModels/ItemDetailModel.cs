namespace ItemDetail
{
/**
    This model contains the item details displayed on the homepage.
    Later, it will be replaced by the ItemModel query with pagination.
*/

     public  class ItemDetailRoot
    {
        public ItemDetaiList Data { get; set; }
    }

    public  class ItemDetaiList
    {
        public List<ItemDetailModel> Items { get; set; }
    }

    public  class ItemDetailModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public Uri WikiLink { get; set; }
        public List<SellFor> SellFor { get; set; }
        public List<BuyFor> BuyFor { get; set; }
        public List<object> BartersUsing { get; set; }
        public List<object> BartersFor { get; set; }
        public List<object> CraftsUsing { get; set; }
        public List<object> CraftsFor { get; set; }
        public List<UsedInTask> UsedInTasks { get; set; }
        public List<ReceivedFromTask> ReceivedFromTasks { get; set; }
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
        public Uri ImageLink { get; set; }
        public List<Level> Levels { get; set; }
    }

    public  class Level
    {
        public long LevelLevel { get; set; }
        public long RequiredPlayerLevel { get; set; }
        public double RequiredReputation { get; set; }
        public long RequiredCommerce { get; set; }
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
        public ItemItem Item { get; set; }
    }

    public  class ItemItem
    {
        public string Name { get; set; }
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
        public ItemItem Item { get; set; }
    }
}

