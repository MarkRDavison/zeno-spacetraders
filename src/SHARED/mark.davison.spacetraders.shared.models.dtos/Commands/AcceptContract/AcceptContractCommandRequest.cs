namespace mark.davison.spacetraders.shared.models.dtos.Commands.AcceptContract;

[PostRequest(Path = "accept-contract-command")]
public sealed class AcceptContractCommandRequest : IdentifiedCommandRequest<AcceptContractCommandRequest, AcceptContractCommandResponse>
{
    public string ContractId { get; set; } = string.Empty;
}
