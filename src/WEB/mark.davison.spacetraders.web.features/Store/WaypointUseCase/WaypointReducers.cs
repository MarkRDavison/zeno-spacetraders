namespace mark.davison.spacetraders.web.features.Store.WaypointUseCase;

public static class WaypointReducers
{
    [ReducerMethod]
    public static WaypointState FetchWaypointsAction(WaypointState state, FetchWaypointsAction action)
    {
        return new WaypointState(true, [], []);
    }

    [ReducerMethod]
    public static WaypointState FetchWaypointsActionResponse(WaypointState state, FetchWaypointsActionResponse response)
    {
        if (response.SuccessWithValue)
        {
            var newIds = response.Value.Select(_ => _.WaypointSymbol).ToHashSet();

            return new WaypointState(
                false,
                [
                    .. state.Waypoints.Where(_ => !newIds.Contains(_.WaypointSymbol)),
                    .. response.Value
                ],
                state.Shipyards);
        }

        return new WaypointState(false, state.Waypoints, state.Shipyards);
    }

    [ReducerMethod]
    public static WaypointState FetchWaypointActionResponse(WaypointState state, FetchWaypointActionResponse response)
    {
        if (response.SuccessWithValue)
        {

            return new WaypointState(
                false,
                [
                    .. state.Waypoints.Where(_ => _.WaypointSymbol != response.Value.WaypointSymbol),
                    response.Value
                ],
                state.Shipyards);
        }

        return new WaypointState(false, state.Waypoints, state.Shipyards);
    }



    [ReducerMethod]
    public static WaypointState FetchShipyardAction(WaypointState state, FetchShipyardAction action)
    {
        return new WaypointState(
            state.IsLoading,
            state.Waypoints,
            state.Shipyards.Where(_ => _.Symbol != action.WaypointSymbol));
    }

    [ReducerMethod]
    public static WaypointState FetchShipyardActionResponse(WaypointState state, FetchShipyardActionResponse response)
    {
        if (response.SuccessWithValue)
        {
            return new WaypointState(
                state.IsLoading,
                state.Waypoints,
                [.. state.Shipyards.Where(_ => _.Symbol != response.Value.Symbol), response.Value]);
        }

        return state;
    }
}
