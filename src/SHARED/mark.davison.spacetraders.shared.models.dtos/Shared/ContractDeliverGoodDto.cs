namespace mark.davison.spacetraders.shared.models.dtos.Shared;

public sealed class ContractDeliverGoodDto
{
    public string TradeSymbol { get; set; } = string.Empty;
    public string DestinationSymbol { get; set; } = string.Empty;
    public int UnitsRequired { get; set; }
    public int UnitsFulfilled { get; set; }
}
