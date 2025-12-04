using System.Collections.Generic;
    public class ItemBaseRoot
    {
        public ItemBaseModel Data { get; set; }
    }
    public partial class ItemBaseModel
    {
        public List<Item> Items { get; set; }
    }

    public partial class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Uri GridImageLink { get; set; }
        public object ChangeLast48H { get; set; }
        public object ChangeLast48HPercent { get; set; }
        public Category Category { get; set; }
        public List<For> SellFor { get; set; }
        public List<For> BuyFor { get; set; }
    }

    public partial class For
    {
        public long PriceRub { get; set; }
        public Vendor Vendor { get; set; }
    }

    public partial class Vendor
    {
        public string Name { get; set; }
    }

    public partial class Category
    {
        public string NormalizedName { get; set; }
    }