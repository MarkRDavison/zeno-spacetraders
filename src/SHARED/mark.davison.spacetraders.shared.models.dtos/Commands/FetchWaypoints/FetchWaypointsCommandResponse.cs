namespace mark.davison.spacetraders.shared.models.dtos.Commands.FetchWaypoints;

public class FetchWaypointsCommandResponse : Response<List<WaypointDto>>
{
    public MetaInfo Meta { get; set; } = new();
}
