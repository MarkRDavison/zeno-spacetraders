namespace mark.davison.spacetraders.web.components.Pages.Waypoint;

public partial class Shipyard
{
    [Parameter, EditorRequired]
    public required string Identifier { get; set; }

    [Parameter, EditorRequired]
    public required ShipyardDto? ShipyardDto { get; set; }

    [Inject]
    public required IStoreHelper StoreHelper { get; set; }

    [Inject]
    public required IClientNavigationManager ClientNavigationManager { get; set; }

    private List<CommandMenuItem> _commandMenuItems =>
        [
            new CommandMenuItem
            {
                Id = "PURCHASE",
                Text = "Purchase"
            }
        ];

    private async Task CommandMenuItemSelected(CommandMenuItem item, ShipyardShipDto shipyardShip)
    {
        if (item.Id == "PURCHASE")
        {
            await PurchaseShip(shipyardShip);
        }
    }

    private async Task PurchaseShip(ShipyardShipDto shipyardShip)
    {
        var action = new PurchaseShipAction
        {
            Identifier = Identifier,
            ShipType = shipyardShip.Type,
            WaypointSymbol = ShipyardDto!.WaypointSymbol
        };

        var response = await StoreHelper.DispatchAndWaitForResponse<PurchaseShipAction, UpdateShipsActionResponse>(action);

        if (response.SuccessWithValue)
        {
            if (response.Value.Select(_ => _.Ship?.ShipSymbol).FirstOrDefault() is string newShipSymbol)
            {
                ClientNavigationManager.NavigateTo(RouteHelpers.Ship(Identifier, newShipSymbol));
            }
        }
    }
}
