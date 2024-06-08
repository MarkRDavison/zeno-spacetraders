namespace mark.davison.spacetraders.shared.models.dtos.Shared;
public sealed class ShipyardShipDto
{
    public string Type { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Supply { get; set; } = string.Empty;
    public int PurchasePrice { get; set; }
}
