public class ItemBaseQueryType
{
    public string Id { get; set; } 
    public string? Name { get; set; }
    public string? GridImageLink { get; set; }
    public int? ChangeLast48h { get; set; }
    public int? ChangeLast48hPercent { get; set; }
    public List<TraderForType> SellFor { get; set; } 
    public List<TraderForType> BuyFor { get; set; }
    public Category Category { get; set; }
}

public class TraderForType
{
    public int? PriceRUB { get; set; }
    public Vendor Vendor { get; set; } 
}

public class Vendor
{
    public string? Name { get; set; } 
}

public class Category
{
    public string? NormalizedName { get; set; }
}
public class CategoriesQueryType
{
    public string? id { get; set; }
    public string? name { get; set; }
    public string? normalizedName { get; set; }
    public List<Children>? children {get;set;}
    public Parent? parent {get;set;}
}

public class Children
{
    public string? normalizedName { get; set; }
}

public class Parent
{
    public string? normalizedName { get; set; }
}