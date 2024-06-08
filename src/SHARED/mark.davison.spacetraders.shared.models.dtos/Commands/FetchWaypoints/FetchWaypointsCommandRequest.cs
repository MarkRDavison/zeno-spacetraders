namespace mark.davison.spacetraders.shared.models.dtos.Commands.FetchWaypoints;

[PostRequest(Path = "fetch-waypoints-command")]
public sealed class FetchWaypointsCommandRequest : ICommand<FetchWaypointsCommandRequest, FetchWaypointsCommandResponse>
{
    public Guid AccountId { get; set; }
    public string SystemSymbol { get; set; } = string.Empty;
    public MetaInfo? Meta { get; set; }
}
