namespace mark.davison.spacetraders.web.features.Store.WaypointUseCase;

public sealed class FetchWaypointsAction : BaseAction
{
    public Guid AccountId { get; set; }
    public string SystemSymbol { get; set; } = string.Empty;
    public MetaInfo Meta { get; set; } = new();
}

public sealed class FetchWaypointsActionResponse : BaseActionResponse<List<WaypointDto>>
{

}