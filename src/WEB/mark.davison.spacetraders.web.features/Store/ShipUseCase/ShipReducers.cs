namespace mark.davison.spacetraders.web.features.Store.ShipUseCase;

public static class ShipReducers
{
    [ReducerMethod]
    public static ShipState FetchShipsAction(ShipState state, FetchShipsAction action)
    {
        return new ShipState(true, [], [], [], [], [], []);
    }

    [ReducerMethod]
    public static ShipState UpdateShipsActionResponse(ShipState state, UpdateShipsActionResponse response)
    {
        if (response.SuccessWithValue)
        {
            var updatedShipSymbols = response.Value.Where(_ => _.Ship is not null).Select(_ => _.Ship!.ShipSymbol).ToHashSet();
            var updatedShipNavSymbols = response.Value.Where(_ => _.ShipNav is not null).Select(_ => _.ShipNav!.ShipSymbol).ToHashSet();
            var updatedShipNavRouteSymbols = response.Value.Where(_ => _.ShipNavRoute is not null).Select(_ => _.ShipNavRoute!.ShipSymbol).ToHashSet();
            var updatedShipCargoSymbols = response.Value.Where(_ => _.ShipCargo is not null).Select(_ => _.ShipCargo!.ShipSymbol).ToHashSet();
            var updatedShipCooldownSymbols = response.Value.Where(_ => _.Cooldown is not null).Select(_ => _.Cooldown!.ShipSymbol).ToHashSet();
            var updatedShipFuelSymbols = response.Value.Where(_ => _.Fuel is not null).Select(_ => _.Fuel!.ShipSymbol).ToHashSet();

            return new ShipState(
                false,
                [
                    .. state.Ships.Where(_ => !updatedShipSymbols.Contains(_.ShipSymbol)),
                    .. response.Value.Select(_ => _.Ship).Where(_ => _ is not null)
                ],
                [
                    .. state.ShipNavs.Where(_ => !updatedShipNavSymbols.Contains(_.ShipSymbol)),
                    .. response.Value.Select(_ => _.ShipNav).Where(_ => _ is not null)
                ],
                [
                    .. state.ShipNavRoutes.Where(_ => !updatedShipNavRouteSymbols.Contains(_.ShipSymbol)),
                    .. response.Value.Select(_ => _.ShipNavRoute).Where(_ => _ is not null)
                ],
                [
                    .. state.ShipCargo.Where(_ => !updatedShipCargoSymbols.Contains(_.ShipSymbol)),
                    .. response.Value.Select(_ => _.ShipCargo).Where(_ => _ is not null)
                ],
                [
                    .. state.ShipCooldown.Where(_ => !updatedShipCooldownSymbols.Contains(_.ShipSymbol)),
                    .. response.Value.Select(_ => _.Cooldown).Where(_ => _ is not null)
                ],
                [
                    ..state.ShipFuel.Where(_ => !updatedShipFuelSymbols.Contains(_.ShipSymbol)),
                    ..response.Value.Select(_ => _.Fuel).Where(_ => _ is not null)
                ]);
        }

        return new ShipState(
            false,
            state.Ships,
            state.ShipNavs,
            state.ShipNavRoutes,
            state.ShipCargo,
            state.ShipCooldown,
            state.ShipFuel);
    }
}
