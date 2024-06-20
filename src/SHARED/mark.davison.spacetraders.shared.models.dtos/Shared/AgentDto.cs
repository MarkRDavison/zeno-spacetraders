namespace mark.davison.spacetraders.shared.models.dtos.Shared;

public sealed class AgentDto
{
    public string Symbol { get; set; } = string.Empty;
    public string Headquarters { get; set; } = string.Empty;
    public string StartingFaction { get; set; } = string.Empty;
    public long Credits { get; set; }
    public int ShipCount { get; set; }
}
