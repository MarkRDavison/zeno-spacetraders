namespace mark.davison.spacetraders.shared.models.dtos.Commands.FetchWaypoint;

[PostRequest(Path = "fetch-waypoint-command")]
public sealed class FetchWaypointCommandRequest : ICommand<FetchWaypointCommandRequest, FetchWaypointCommandResponse>
{
    public Guid AccountId { get; set; }
    public string WaypointSymbol { get; set; } = string.Empty;
}
