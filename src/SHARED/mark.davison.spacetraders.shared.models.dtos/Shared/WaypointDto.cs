namespace mark.davison.spacetraders.shared.models.dtos.Shared;

public sealed class WaypointDto
{
    public string WaypointSymbol { get; set; } = string.Empty;
    public string SystemSymbol { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int X { get; set; }
    public int Y { get; set; }
    public List<string> Traits { get; set; } = [];
}
