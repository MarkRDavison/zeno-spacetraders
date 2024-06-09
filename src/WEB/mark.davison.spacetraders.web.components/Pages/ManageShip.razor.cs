namespace mark.davison.spacetraders.web.components.Pages;

public partial class ManageShip
{
    [Parameter]
    public required string ShipSymbol { get; set; }

    [Inject]
    public required IState<ShipState> ShipState { get; set; }

    [Inject]
    public required IStoreHelper StoreHelper { get; set; }

    [Inject]
    public required IAccountContextService AccountContextService { get; set; }

    private ShipDto? ShipDto => ShipState.Value.Ships.FirstOrDefault(_ => _.Symbol == ShipSymbol);

    protected override async Task OnParametersSetAsync()
    {
        if (ShipDto == null &&
            !string.IsNullOrEmpty(ShipSymbol) &&
            AccountContextService.GetActiveAccount() is { } account)
        {
            await StoreHelper.DispatchAndWaitForResponse<FetchShipAction, FetchShipActionResponse>(new FetchShipAction
            {
                AccountId = account.Id,
                ShipSymbol = ShipSymbol
            });
        }
    }
}
