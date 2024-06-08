namespace mark.davison.spacetraders.web.features.Store.ShipUseCase;

public static class ShipReducers
{
    [ReducerMethod]
    public static ShipState FetchShipsAction(ShipState state, FetchShipsAction action)
    {
        return new ShipState(true, []);
    }

    [ReducerMethod]
    public static ShipState FetchShipsActionResponse(ShipState state, FetchShipsActionResponse response)
    {
        if (response.SuccessWithValue)
        {
            var newIds = response.Value.Select(_ => _.Symbol).ToHashSet();

            return new ShipState(false,
                [
                    .. state.Ships.Where(_ => !newIds.Contains(_.Symbol)),
                    .. response.Value
                ]);
        }

        return new ShipState(false, state.Ships);
    }
}
