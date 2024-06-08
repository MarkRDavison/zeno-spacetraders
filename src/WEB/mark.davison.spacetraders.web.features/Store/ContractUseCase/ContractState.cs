namespace mark.davison.spacetraders.web.features.Store.ContractUseCase;

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
}
