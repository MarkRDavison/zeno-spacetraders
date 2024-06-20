namespace mark.davison.spacetraders.web.features.Store.ContractUseCase;

public sealed class OldFetchContractsAction : BaseAction
{
    public Guid AccountId { get; set; }
    public MetaInfo Meta { get; set; } = new();
}

public sealed class FetchContractsActionResponse : BaseActionResponse<List<ContractDto>>
{

}

public sealed class AcceptContractAction : BaseAction
{
    public Guid AccountId { get; set; }
    public string ContractId { get; set; } = string.Empty;
}

public sealed class AcceptContractActionResponse : BaseActionResponse<ContractDto>
{

}

public sealed class FetchContractsAction : PaginatedIdentifiedAction;

public sealed class FetchContractAction : IdentifiedAction
{
    public string ContractId { get; set; } = string.Empty;
}

public sealed class UpdateContractsActionResponse : BaseActionResponse<List<ContractDto>>;