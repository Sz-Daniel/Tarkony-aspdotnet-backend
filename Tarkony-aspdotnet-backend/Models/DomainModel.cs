public class ItemBaseResultType
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string IconURL { get; set; } = "";
    public PriceDeal? BestSeller { get; set; }
    public PriceDeal? BestBuy { get; set; }
    public int ChangePrice { get; set; } = 0;
    public int ChangePercent { get; set; } =0;
    public string Category { get; set; } = "";
}

public class PriceDeal
{
    public int Price { get; set; }
    public string Place { get; set; } = "";
}
