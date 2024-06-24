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
        return new ShipState(true, []);
    }

    [DesktopReducer]
    public static ShipState HandleUpdateShipsActionResponse(ShipState state, UpdateShipsActionResponse response)
    {
        if (response.SuccessWithValue)
        {
            return new ShipState(false, response.Value.Select(_ => _.Ship).OfType<ShipDto>());
        }

        return new ShipState(false, []);
    }
}
