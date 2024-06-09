namespace mark.davison.spacetraders.web.components.Pages;

public partial class ManageContract
{
    [Parameter]
    public required string ContractId { get; set; }

    [Inject]
    public required IState<ContractState> ContractState { get; set; }

    [Inject]
    public required IStoreHelper StoreHelper { get; set; }

    [Inject]
    public required IAccountContextService AccountContextService { get; set; }

    private ContractDto? ContractDto => ContractState.Value.Contracts.FirstOrDefault(_ => _.Id == ContractId);

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

}
