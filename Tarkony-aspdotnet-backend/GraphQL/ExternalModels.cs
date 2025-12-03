public class ItemBaseQueryType
{
    public string? Id { get; set; } = "";
    public string? Name { get; set; } = "";
    public string? GridImageLink { get; set; } = "";
    public int? ChangeLast48h { get; set; }
    public int? ChangeLast48hPercent { get; set; }
    public List<TraderForType>? SellFor { get; set; } = new();
    public List<TraderForType>? BuyFor { get; set; } = new();
    public Category? Category { get; set; } = new();
}

public class TraderForType
{
    public int PriceRUB { get; set; } = 0;
    public Vendor? Vendor { get; set; } = new();
}

public class Vendor
{
    public string? Name { get; set; } = "";
}

public class Category
{
    public string? NormalizedName { get; set; } = "";
}
