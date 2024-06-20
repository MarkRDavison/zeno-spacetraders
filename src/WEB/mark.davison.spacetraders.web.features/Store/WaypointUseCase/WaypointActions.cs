namespace mark.davison.spacetraders.web.features.Store.WaypointUseCase;

public sealed class FetchWaypointsAction : PaginatedIdentifiedAction
{
    public string SystemSymbol { get; set; } = string.Empty;
    public string? WaypointType { get; set; }
    public string? WaypointTrait { get; set; }
}

public sealed class FetchWaypointAction : IdentifiedAction
{
    public string SystemSymbol { get; set; } = string.Empty;
    public string WaypointSymbol { get; set; } = string.Empty;
}

public sealed class FetchShipyardAction : IdentifiedAction
{
    public string SystemSymbol { get; set; } = string.Empty;
    public string WaypointSymbol { get; set; } = string.Empty;
}

public sealed class FetchMarketplaceAction : IdentifiedAction
{
    public string SystemSymbol { get; set; } = string.Empty;
    public string WaypointSymbol { get; set; } = string.Empty;
}

public sealed class UpdateWaypointsActionResponse : BaseActionResponse<List<WaypointResponse>>;