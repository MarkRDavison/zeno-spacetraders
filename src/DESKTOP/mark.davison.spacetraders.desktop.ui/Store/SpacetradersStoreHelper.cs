namespace mark.davison.spacetraders.desktop.ui.Store;

internal sealed class SpacetradersStoreHelper : DesktopStoreHelper, ISpacetradersStoreHelper
{
    private readonly IAccountService _accountService;

    public SpacetradersStoreHelper(
        IDesktopStateDispatcher dispatcher,
        IDesktopActionSubscriber actionSubscriber,
        IAccountService accountService
    ) : base(
        dispatcher,
        actionSubscriber)
    {
        _accountService = accountService;
    }

    public async Task EnsureContractsLoadedAsync(IState<ContractState> state, CancellationToken cancellationToken = default)
    {
        if (!state.Value.Loaded && !state.Value.Loading)
        {
            await DispatchAndWaitForResponse<FetchContractsAction, UpdateContractsActionResponse>(new FetchContractsAction
            {
                Identifier = _accountService.ActiveAccountIdentifier
            });
        }
    }
}
