namespace mark.davison.spacetraders.shared.models.dtos.Commands.FetchContracts;

[PostRequest(Path = "fetch-contracts-command")]
public sealed class FetchContractsCommandRequest : ICommand<FetchContractsCommandRequest, FetchContractsCommandResponse>
{
    public Guid AccountId { get; set; }
    public MetaInfo? Meta { get; set; }
}
