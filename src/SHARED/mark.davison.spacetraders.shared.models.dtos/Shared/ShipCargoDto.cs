namespace mark.davison.spacetraders.shared.models.dtos.Shared;

public sealed class ShipCargoDto
{
    public string ShipSymbol { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public int Units { get; set; }
    public List<ShipCargoItemDto> Inventory { get; set; } = [];
}


public sealed class ShipCargoItemDto
{
    public string Symbol { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Units { get; set; }
}