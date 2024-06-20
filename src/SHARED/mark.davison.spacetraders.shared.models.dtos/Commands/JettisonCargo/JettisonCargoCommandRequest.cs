namespace mark.davison.spacetraders.shared.models.dtos.Commands.JettisonCargo;

[PostRequest(Path = "jettison-cargo-command")]
public sealed class JettisonCargoCommandRequest : IdentifiedCommandRequest<JettisonCargoCommandRequest, JettisonCargoCommandResponse>
{
    public string ShipSymbol { get; set; } = string.Empty;
    public string TradeSymbol { get; set; } = string.Empty;
    public int Units { get; set; }
}
