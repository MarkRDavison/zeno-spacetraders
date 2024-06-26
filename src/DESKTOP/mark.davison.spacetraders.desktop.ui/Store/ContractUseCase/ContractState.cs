namespace mark.davison.spacetraders.desktop.ui.Store.ContractUseCase;

public sealed class ContractState : IDesktopState
{
    public ContractState() : this(false, false, [])
    {

    }

    public ContractState(bool loading, bool loaded, IEnumerable<ContractDto> contracts)
    {
        Loading = loading;
        Loaded = loaded;
        Contracts = new([.. contracts]);
    }

    public bool Loading { get; }
    public bool Loaded { get; }

    public ReadOnlyCollection<ContractDto> Contracts { get; }
}
