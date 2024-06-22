namespace mark.davison.spacetraders.shared.models.dtos.Shared;

public sealed class WaypointDto
{
    public string SystemSymbol { get; set; } = string.Empty;
    public string WaypointSymbol { get; set; } = string.Empty;
    public string Orbits { get; set; } = string.Empty;
    public int X { get; set; }
    public int Y { get; set; }
    public List<WaypointTraitDto> Traits { get; set; } = [];
}

public sealed class WaypointTraitDto
{
    public string Symbol { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}