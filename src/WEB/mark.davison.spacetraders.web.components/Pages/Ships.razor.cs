namespace mark.davison.spacetraders.web.components.Pages;

public partial class Ships
{
    [Parameter]
    public required string Identifier { get; set; }

    [Inject]
    public required IStoreHelper StoreHelper { get; set; }

    [Inject]
    public required IState<ShipState> ShipState { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (!ShipState.Value.IsLoading && !ShipState.Value.Ships.Any())
        {
            await StoreHelper.DispatchAndWaitForResponse<FetchShipsAction, UpdateShipsActionResponse>(new()
            {
                Identifier = Identifier,
                Limit = 20
            });
        }
    }
}
