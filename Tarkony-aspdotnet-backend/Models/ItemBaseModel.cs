
using System.Collections.Generic;
    public class ItemBaseRoot
    {
        public ItemBaseList Data { get; set; }
    }
    public  class ItemBaseList
    {
        public List<ItemBaseModel> Items { get; set; }
    }

    public  class ItemBaseModel
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

    public  class For
    {
        public long PriceRub { get; set; }
        public Vendor Vendor { get; set; }
    }

    public  class Vendor
    {
        public string Name { get; set; }
    }

