namespace mark.davison.spacetraders.web.features.Store.WaypointUseCase;

public static class WaypointReducers
{
    [ReducerMethod]
    public static WaypointState FetchWaypointsAction(WaypointState state, FetchWaypointsAction action)
    {
        return new WaypointState(true, []);
    }

    [ReducerMethod]
    public static WaypointState FetchWaypointsActionResponse(WaypointState state, FetchWaypointsActionResponse response)
    {
        if (response.SuccessWithValue)
        {
            var newIds = response.Value.Select(_ => _.Symbol).ToHashSet();

            return new WaypointState(false,
            [
                .. state.Waypoints.Where(_ => !newIds.Contains(_.Symbol)),
                .. response.Value
            ]);
        }

        return new WaypointState(false, state.Waypoints);
    }
}
