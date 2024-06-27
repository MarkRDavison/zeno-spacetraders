namespace mark.davison.spacetraders.desktop.ui.Store.ContractUseCase;

public sealed class FetchContractsAction : IdentifiedBaseAction;

public sealed class NegotiateContractAction : IdentifiedBaseAction
{
    public string ShipSymbol { get; set; } = string.Empty;
}

public sealed class AcceptContractAction : IdentifiedBaseAction
{
    public string ContractId { get; set; } = string.Empty;
}

public sealed class UpdateContractsActionResponse : BaseActionResponse<List<ContractDto>>;