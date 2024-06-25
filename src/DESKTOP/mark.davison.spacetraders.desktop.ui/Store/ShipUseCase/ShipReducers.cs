namespace mark.davison.spacetraders.desktop.ui.Store.ShipUseCase;

public static class ShipReducers
{
    [DesktopReducer]
    public static ShipState HandleResetStateAction(ShipState state, ResetStateAction action)
    {
        return new();
    }

    [DesktopReducer]
    public static ShipState HandleFetchShipsAction(ShipState state, FetchShipsAction response)
    {
        return new ShipState(true, [], []);
    }

    [DesktopReducer]
    public static ShipState HandleUpdateShipsActionResponse(ShipState state, UpdateShipsActionResponse response)
    {
        if (response.SuccessWithValue)
        {
            var newShipSymbols = response.Value.Select(_ => _.Ship).OfType<ShipDto>().Select(_ => _.Symbol).ToHashSet();
            var newShipNavSymbols = response.Value.Select(_ => _.ShipNav).OfType<ShipNavDto>().Select(_ => _.ShipSymbol).ToHashSet();

            return new ShipState(
                false,
                [
                    ..state.Ships.Where(_ => !newShipSymbols.Contains(_.Symbol)),
                    ..response.Value.Select(_ => _.Ship).OfType<ShipDto>()
                ],
                [
                    ..state.ShipNavs.Where(_ => !newShipNavSymbols.Contains(_.ShipSymbol)),
                    ..response.Value.Select(_ => _.ShipNav).OfType<ShipNavDto>()
                ]);
        }

        return new ShipState(false, [], []);
    }
}
