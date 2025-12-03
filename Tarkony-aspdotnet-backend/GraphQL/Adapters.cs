public static class ItemAdapter
{
    public static ItemBaseResultType Map(ItemBaseQueryType item)
    {
        var bestSeller = BestSellerCalc(item.SellFor ?? new List<TraderForType>());
        var bestBuy = BestBuyCalc(item.BuyFor ?? new List<TraderForType>());

        return new ItemBaseResultType
        {
            Id = item.Id ?? "",
            Name = item.Name ?? "",
            IconURL = item.GridImageLink ?? "",
            BestSeller = bestSeller,
            BestBuy = bestBuy,
            ChangePrice = item.ChangeLast48h ?? 0,
            ChangePercent = item.ChangeLast48hPercent ?? 0,
            Category = item.Category?.NormalizedName ?? ""
        };
    }

    private static PriceDeal? BestSellerCalc(List<TraderForType> sellers)
    {
        if (sellers == null || sellers.Count == 0) return null;
        var best = sellers.Aggregate((max, cur) => cur.PriceRUB > max.PriceRUB ? cur : max);
        return new PriceDeal { Price = best.PriceRUB, Place = best.Vendor?.Name ?? "" };
    }

    private static PriceDeal? BestBuyCalc(List<TraderForType> buyers)
    {
        if (buyers == null || buyers.Count == 0) return null;
        var best = buyers.Aggregate((min, cur) => cur.PriceRUB < min.PriceRUB ? cur : min);
        return new PriceDeal { Price = best.PriceRUB, Place = best.Vendor?.Name ?? "" };
    }
}
