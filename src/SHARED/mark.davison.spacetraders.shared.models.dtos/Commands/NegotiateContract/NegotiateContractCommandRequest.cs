namespace mark.davison.spacetraders.shared.models.dtos.Commands.NegotiateContract;

[PostRequest(Path = "negotiate-contract-command")]
public sealed class NegotiateContractCommandRequest : IdentifiedCommandRequest<NegotiateContractCommandRequest, NegotiateContractCommandResponse>
{
    public string ShipSymbol { get; set; } = string.Empty;
}
