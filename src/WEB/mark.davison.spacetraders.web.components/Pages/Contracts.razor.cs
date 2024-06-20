namespace mark.davison.spacetraders.web.components.Pages;

public partial class Contracts
{
    [Parameter]
    public required string Identifier { get; set; }
    [Inject]
    public required IState<ContractState> ContractState { get; set; }

    [Inject]
    public required IStoreHelper StoreHelper { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await LoadState();
    }

    private async Task LoadState()
    {
        if (!string.IsNullOrEmpty(Identifier))
        {
            await StoreHelper.DispatchAndWaitForResponse<FetchContractsAction, UpdateContractsActionResponse>(new()
            {
                Identifier = Identifier
            });
        }
    }
}
