namespace mark.davison.spacetraders.shared.models.dtos.Commands.SellCargo;

[PostRequest(Path = "sell-cargo-command")]
public sealed class SellCargoCommandRequest : IdentifiedCommandRequest<SellCargoCommandRequest, SellCargoCommandResponse>
{
    public string ShipSymbol { get; set; } = string.Empty;
    public string TradeSymbol { get; set; } = string.Empty;
    public int Units { get; set; }
}
