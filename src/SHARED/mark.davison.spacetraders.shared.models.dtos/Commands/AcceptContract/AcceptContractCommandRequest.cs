namespace mark.davison.spacetraders.shared.models.dtos.Commands.AcceptContract;

[PostRequest(Path = "accept-contract-command")]
public sealed class AcceptContractCommandRequest : ICommand<AcceptContractCommandRequest, AcceptContractCommandResponse>
{
    public Guid AccountId { get; set; }
    public string ContractId { get; set; } = string.Empty;
}
