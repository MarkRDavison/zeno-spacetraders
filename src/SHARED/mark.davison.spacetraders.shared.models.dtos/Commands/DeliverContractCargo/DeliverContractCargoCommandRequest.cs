namespace mark.davison.spacetraders.shared.models.dtos.Commands.DeliverCargoContract;

[PostRequest(Path = "deliver-contract-cargo-command")]
public sealed class DeliverContractCargoCommandRequest : IdentifiedCommandRequest<DeliverContractCargoCommandRequest, DeliverContractCargoCommandResponse>
{
    public string ContractId { get; set; } = string.Empty;
    public string ShipSymbol { get; set; } = string.Empty;
    public string TradeSymbol { get; set; } = string.Empty;
    public int Units { get; set; }
}
