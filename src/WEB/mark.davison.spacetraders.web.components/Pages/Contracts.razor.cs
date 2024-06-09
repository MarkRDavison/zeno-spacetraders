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

    // TODO: Consolidate command menu and actions
    private List<CommandMenuItem> _commandMenuItems { get; } =
        [
            new CommandMenuItem
            {
                Id = "ACCEPT",
                Text = "Accept"
            }
        ];

    private async Task CommandMenuItemSelected(CommandMenuItem item, ContractDto contract)
    {
        if (AccountContextService.GetActiveAccount() is { } account)
        {
            if (item.Id == "ACCEPT" && !contract.Accepted)
            {
                await AcceptContract(contract, account.Id);
            }
        }
    }

    private async Task AcceptContract(ContractDto contract, Guid accountId)
    {
        await StoreHelper.DispatchAndWaitForResponse<AcceptContractAction, AcceptContractActionResponse>(new AcceptContractAction
        {
            AccountId = accountId,
            ContractId = contract.Id
        });
    }

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
