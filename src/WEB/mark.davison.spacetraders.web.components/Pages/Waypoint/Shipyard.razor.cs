namespace mark.davison.spacetraders.web.components.Pages.Waypoint;
public partial class Shipyard
{
    [Parameter, EditorRequired]
    public ShipyardDto? ShipyardDto { get; set; }

    [Inject]
    public required IState<AccountState> AccountState { get; set; }

    [Inject]
    public required IStoreHelper StoreHelper { get; set; }

    [Inject]
    public required IAccountContextService AccountContextService { get; set; }

    [Inject]
    public required IClientNavigationManager ClientNavigationManager { get; set; }

    [Inject]
    public required ISnackbar Snackbar { get; set; }

    private List<CommandMenuItem> _commandMenuItems { get; } =
        [
            new CommandMenuItem
            {
                Id = "PURCHASE",
                Text = "Purchase"
            }
        ];

    private async Task CommandMenuItemSelected(CommandMenuItem item, ShipyardShipDto shipyardShip)
    {
        if (AccountContextService.GetActiveAccount() is { } account && ShipyardDto is not null) // TODO: Better way of getting account id
        {
            if (item.Id == "PURCHASE")
            {
                await PurchaseShip(shipyardShip, account.Id);
            }
        }
    }

    private async Task PurchaseShip(ShipyardShipDto shipyardShip, Guid accountId)
    {
        var action = new PurchaseShipAction
        {
            AccountId = accountId,
            ShipType = shipyardShip.Type,
            WaypointSymbol = ShipyardDto!.Symbol
        };

        var response = await StoreHelper.DispatchAndWaitForResponse<PurchaseShipAction, PurchaseShipActionResponse>(action);

        if (response.SuccessWithValue)
        {
            ClientNavigationManager.NavigateTo(RouteHelpers.Ship(response.Value.Symbol));
        }
        else
        {
            foreach (var s in response.Errors)
            {
                Snackbar.Add(s, Severity.Error);
            }
        }
    }
}
