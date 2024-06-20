namespace mark.davison.spacetraders.web.components.Pages;

public partial class ManageContract
{
    [Parameter]
    public required string Identifier { get; set; }

    [Parameter]
    public required string ContractId { get; set; }

    [Inject]
    public required IState<ContractState> ContractState { get; set; }

    [Inject]
    public required IStoreHelper StoreHelper { get; set; }

    private ContractDto? ActiveContract => ContractState.Value.GetContract(ContractId);

    protected override async Task OnParametersSetAsync()
    {
        await LoadState();
    }

    private async Task LoadState()
    {
        if (!string.IsNullOrEmpty(Identifier))
        {
            await StoreHelper.DispatchAndWaitForResponse<FetchContractAction, UpdateContractsActionResponse>(new()
            {
                Identifier = Identifier,
                ContractId = ContractId
            });
        }
    }

    private List<CommandMenuItem> _commandMenuItems =>
        [
            new CommandMenuItem
            {
                Id = "ACCEPT",
                Text = "Accept",
                Disabled = ActiveContract!.Accepted
            }
        ];

    private async Task CommandMenuItemSelected(CommandMenuItem item)
    {
        if (ActiveContract is null) { return; }

        await Task.CompletedTask;
        if (item.Id == "ACCEPT" && !ActiveContract.Accepted)
        {

        }
    }
}
