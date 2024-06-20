namespace mark.davison.spacetraders.shared.models.dtos.Shared;

public sealed class MarketplaceDto
{
    public string WaypointSymbol { get; set; } = string.Empty;
    public List<TradeGoodDto> Exports { get; set; } = [];
    public List<TradeGoodDto> Imports { get; set; } = [];
    public List<MarketTradeGoodDto> TradeGoods { get; set; } = [];
}
