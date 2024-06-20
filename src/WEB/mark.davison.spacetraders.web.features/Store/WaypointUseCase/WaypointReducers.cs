namespace mark.davison.spacetraders.web.features.Store.WaypointUseCase;

public static class WaypointReducers
{
    [ReducerMethod]
    public static WaypointState FetchWaypointsAction(WaypointState state, FetchWaypointsAction action)
    {
        return new WaypointState(true, [], [], []);
    }

    [ReducerMethod]
    public static WaypointState UpdateWaypointsActionResponse(WaypointState state, UpdateWaypointsActionResponse response)
    {
        if (response.SuccessWithValue)
        {
            var updatedWaypointSymbols = response.Value.Where(_ => _.Waypoint is not null).Select(_ => _.Waypoint!.WaypointSymbol);
            var updatedShipyardSymbols = response.Value.Where(_ => _.Shipyard is not null).Select(_ => _.Shipyard!.WaypointSymbol);
            var updatedMarketplaceSymbols = response.Value.Where(_ => _.Marketplace is not null).Select(_ => _.Marketplace!.WaypointSymbol);

            return new WaypointState(
                false,
                [
                    .. state.Waypoints.Where(_ => !updatedWaypointSymbols.Contains(_.WaypointSymbol)),
                    .. response.Value.Select(_ => _.Waypoint).Where(_ => _ is not null)
                ],
                [
                    ..state.Shipyards.Where(_ => !updatedShipyardSymbols.Contains(_.WaypointSymbol)),
                    ..response.Value.Select(_ => _.Shipyard).Where(_ => _ is not null)
                ],
                [
                    ..state.Marketplaces.Where(_ => !updatedMarketplaceSymbols.Contains(_.WaypointSymbol)),
                    ..response.Value.Select(_ => _.Marketplace).Where(_ => _ is not null)
                ]);
        }

        return new(false, state.Waypoints, state.Shipyards, state.Marketplaces);
    }
}
