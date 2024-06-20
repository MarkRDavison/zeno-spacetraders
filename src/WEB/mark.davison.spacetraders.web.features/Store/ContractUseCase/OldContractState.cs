namespace mark.davison.spacetraders.web.features.Store.ContractUseCase;

[FeatureState]
public sealed class OldContractState
{
    public bool IsLoading { get; }
    public ReadOnlyCollection<ContractDto> Contracts { get; }

    public OldContractState() : this(false, [])
    {

    }

    public OldContractState(bool isLoading, IEnumerable<ContractDto> contracts)
    {
        IsLoading = isLoading;
        Contracts = new ReadOnlyCollection<ContractDto>(contracts.ToList());
    }
}

[FeatureState]
public sealed class ContractState
{
    public bool IsLoading { get; }
    public ReadOnlyCollection<ContractDto> Contracts { get; }

    public ContractState() : this(false, [])
    {

    }

    public ContractState(bool isLoading, IEnumerable<ContractDto> contracts)
    {
        IsLoading = isLoading;
        Contracts = new ReadOnlyCollection<ContractDto>(contracts.ToList());
    }

    public ContractDto? GetContract(string contractId) => Contracts.FirstOrDefault(_ => _.Id == contractId);
}
