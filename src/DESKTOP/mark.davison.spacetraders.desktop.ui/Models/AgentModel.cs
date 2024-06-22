namespace mark.davison.spacetraders.desktop.ui.Models;

public sealed class AgentModel
{
    public string Symbol { get; set; } = string.Empty;
    public string Headquarters { get; set; } = string.Empty;
    public long Credits { get; set; }
    public int ShipCount { get; set; }
}
