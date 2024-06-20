namespace mark.davison.spacetraders.shared.models.dtos.Shared;

public sealed class ShipFuelDto
{
    public string ShipSymbol { get; set; } = string.Empty;
    public int Current { get; set; }
    public int Capacity { get; set; }
}
