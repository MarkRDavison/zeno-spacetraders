namespace mark.davison.spacetraders.shared.models.dtos.Shared;

public sealed class ShipyardTransactionDto
{
    public string Type { get; set; } = string.Empty;
    public int Price { get; set; }
    public string AgentSymbol { get; set; } = string.Empty;
}
