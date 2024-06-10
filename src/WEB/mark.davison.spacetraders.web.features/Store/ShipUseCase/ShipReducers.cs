namespace mark.davison.spacetraders.web.features.Store.ShipUseCase;

public static class ShipReducers
{
    [ReducerMethod]
    public static ShipState FetchShipsAction(ShipState state, FetchShipsAction action)
    {
        return new ShipState(true, [], []);
    }

    [ReducerMethod]
    public static ShipState FetchShipsActionResponse(ShipState state, FetchShipsActionResponse response)
    {
        if (response.SuccessWithValue)
        {
            var newIds = response.Value.Select(_ => _.Symbol).ToHashSet();

            return new ShipState(
                false,
                [
                    .. state.Ships.Where(_ => !newIds.Contains(_.Symbol)),
                    .. response.Value
                ], state.Cargo);
        }

        return new ShipState(false, state.Ships, state.Cargo);
    }

    [ReducerMethod]
    public static ShipState PurchaseShipActionResponse(ShipState state, PurchaseShipActionResponse response)
    {
        if (response.SuccessWithValue)
        {
            return new ShipState(state.IsLoading, [.. state.Ships, response.Value], state.Cargo);
        }

        return state;
    }

    [ReducerMethod]
    public static ShipState FetchShipActionResponse(ShipState state, FetchShipActionResponse response)
    {
        if (response.SuccessWithValue)
        {
            return new ShipState(state.IsLoading, [.. state.Ships.Where(_ => _.Symbol != response.Value.Symbol), response.Value], state.Cargo);
        }

        return state;
    }

    [ReducerMethod]
    public static ShipState UpdateShipNavResponse(ShipState state, UpdateShipNavResponse response)
    {
        if (response.SuccessWithValue)
        {
            var existingShip = state.Ships.FirstOrDefault(_ => _.Symbol == response.ShipSymbol);

            if (existingShip != null)
            {
                existingShip.Nav = response.Value;

                return new ShipState(
                    state.IsLoading,
                    [.. state.Ships.Where(_ => _.Symbol != existingShip.Symbol), existingShip],
                    state.Cargo);
            }
        }

        return state;
    }

    [ReducerMethod]
    public static ShipState UpdateShipFuelResponse(ShipState state, UpdateShipFuelResponse response)
    {
        if (response.SuccessWithValue)
        {
            var existingShip = state.Ships.FirstOrDefault(_ => _.Symbol == response.ShipSymbol);

            if (existingShip != null)
            {
                existingShip.Fuel = response.Value;

                return new ShipState(
                    state.IsLoading,
                    [.. state.Ships.Where(_ => _.Symbol != existingShip.Symbol), existingShip],
                    state.Cargo);
            }
        }

        return state;
    }

    [ReducerMethod]
    public static ShipState FetchShipCargoActionResponse(ShipState state, FetchShipCargoActionResponse response)
    {
        if (response.SuccessWithValue)
        {
            return new ShipState(
                state.IsLoading,
                state.Ships,
                [.. state.Cargo.Where(_ => _.ShipSymbol != response.Value.ShipSymbol), response.Value]);
        }

        return state;
    }
}
