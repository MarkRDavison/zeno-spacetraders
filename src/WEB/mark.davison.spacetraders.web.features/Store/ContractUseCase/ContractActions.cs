namespace mark.davison.spacetraders.web.features.Store.ContractUseCase;

public sealed class FetchContractsAction : BaseAction
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