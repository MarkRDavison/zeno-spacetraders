namespace mark.davison.spacetraders.shared.models.dtos.Shared;

public sealed class MarketTradeGoodDto
{
    public string TradeSymbol { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int TradeVolume { get; set; }
    public string Supply { get; set; } = string.Empty;
    public string Activity { get; set; } = string.Empty;
    public int PurchasePrice { get; set; }
    public int SellPrice { get; set; }
}
