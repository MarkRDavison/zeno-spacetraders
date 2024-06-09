﻿namespace mark.davison.spacetraders.web.components.Pages.Ship;

public partial class ShipNavigation
{
    [Parameter, EditorRequired]
    public required ShipDto ShipDto { get; set; }

    [Inject]
    public required IAccountContextService AccountContextService { get; set; }

    [Inject]
    public required IStoreHelper StoreHelper { get; set; }

    private async Task MoveShipToOrbit()
    {
        if (AccountContextService.GetActiveAccount() is { } account)
        {
            await StoreHelper.DispatchAndWaitForResponse<OrbitShipAction, OrbitShipActionResponse>(new OrbitShipAction
            {
                AccountId = account.Id,
                ShipSymbol = ShipDto.Symbol
            });
        }
    }
}
