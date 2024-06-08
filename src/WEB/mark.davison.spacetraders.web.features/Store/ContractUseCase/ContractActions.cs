namespace mark.davison.spacetraders.web.features.Store.ContractUseCase;

public sealed class FetchContractsAction : BaseAction
{
    public Guid AccountId { get; set; }
    public MetaInfo Meta { get; set; } = new();
}

public sealed class FetchContractsActionResponse : BaseActionResponse<List<ContractDto>>
{

}

