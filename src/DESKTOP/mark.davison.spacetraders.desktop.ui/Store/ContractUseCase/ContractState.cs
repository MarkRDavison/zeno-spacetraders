namespace mark.davison.spacetraders.desktop.ui.Store.ContractUseCase;

public sealed class ContractState : IDesktopState
{
    public ContractState() : this(false, [])
    {

    }

    public ContractState(bool loading, IEnumerable<ContractDto> contracts)
    {
        Loading = loading;
        Contracts = new([.. contracts]);
    }

    public bool Loading { get; }

    public ReadOnlyCollection<ContractDto> Contracts { get; }
}
