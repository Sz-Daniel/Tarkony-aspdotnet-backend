using Items.Domain;

namespace Contracts
{
    namespace ItemBase
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
        }
    }

    namespace ItemDetail
    {
        public class Items
        {
            public string id { get; set; }
            public string name { get; set; }
            public string normalizedName { get; set; }
            public string wiki { get; set; }
            public List<SellTo> sellTo { get; set; }
            public List<BuyFrom> buyFrom { get; set; }
            public List<Barter> barterInput { get; set; }
            public List<Barter> barterOutput { get; set; }
            public List<Craft> craftInput { get; set; }
            public List<Craft> craftOutput { get; set; }
            public List<TaskNeed>? taskNeed { get; set; }
            public List<TaskGive>? taskGive { get; set; }
        }
    }
}
