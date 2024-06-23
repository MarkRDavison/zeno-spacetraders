using System.Linq;

namespace mark.davison.spacetraders.desktop.ui.Store.ShipUseCase;

public class ShipReducers : StateReducer<ShipState>
{
    public ShipReducers()
    {
        RegisterAction<FetchShipsAction>(HandleFetchShipsAction);
        RegisterAction<ResetStateAction>(HandleResetStateAction);
        RegisterResponse<UpdateShipsActionResponse>(HandleUpdateShipsActionResponse);
    }

    public static ShipState HandleResetStateAction(ShipState state, ResetStateAction action)
    {
        return new ShipState();
    }

    public static ShipState HandleFetchShipsAction(ShipState state, FetchShipsAction response)
    {
        return state;
    }

    public static ShipState HandleUpdateShipsActionResponse(ShipState state, UpdateShipsActionResponse response)
    {
        if (response.SuccessWithValue)
        {
            return new ShipState(response.Value.Select(_ => _.Ship).OfType<ShipDto>());
        }

        return state;
    }
}
