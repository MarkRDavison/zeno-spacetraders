namespace mark.davison.spacetraders.avalonia.ui.Models;

public sealed class WaypointInfoModel
{
    private readonly Waypoint _waypoint;
    public WaypointInfoModel(Waypoint waypoint)
    {
        _waypoint = waypoint;
    }

    public string Symbol => _waypoint.Symbol;
    public string Type => _waypoint.Type.ToString();
    public IEnumerable<string> Traits => _waypoint.Traits.Select(_ => _.Name).Order();
}
