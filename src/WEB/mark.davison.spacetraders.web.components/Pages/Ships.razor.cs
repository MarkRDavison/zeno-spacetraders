namespace mark.davison.spacetraders.web.components.Pages;

public partial class Ships
{
    [Inject]
    public required IState<ShipState> ShipState { get; set; }

    [Inject]
    public required IState<AccountState> AccountState { get; set; }

    [Inject]
    public required IStoreHelper StoreHelper { get; set; }

    [Inject]
    public required IAccountContextService AccountContextService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await LoadState();
    }

    private async Task LoadState()
    {
        if (AccountContextService.GetActiveAccount() is { } account)
        {
            await StoreHelper.DispatchAndWaitForResponse<FetchShipsAction, FetchShipsActionResponse>(
                new FetchShipsAction
                {
                    AccountId = account.Id,
                    Meta = new MetaInfo
                    {
                        Limit = 20
                    }
                });
        }
    }
}
