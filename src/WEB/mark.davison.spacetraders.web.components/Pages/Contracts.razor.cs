namespace mark.davison.spacetraders.web.components.Pages;

public partial class Contracts
{
    [Inject]
    public required IState<ContractState> ContractState { get; set; }

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
            await StoreHelper.DispatchAndWaitForResponse<FetchContractsAction, FetchContractsActionResponse>(
                new FetchContractsAction
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
