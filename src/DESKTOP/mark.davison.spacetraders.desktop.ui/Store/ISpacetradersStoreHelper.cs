namespace mark.davison.spacetraders.desktop.ui.Store;

public interface ISpacetradersStoreHelper : IStoreHelper
{
    Task EnsureContractsLoadedAsync(IState<ContractState> state, CancellationToken cancellationToken = default);
}
