namespace mark.davison.spacetraders.shared.models.dtos.Shared;

public sealed class ShipyardDto
{
    public string Symbol { get; set; } = string.Empty;
    public int ModificationsFee { get; set; }
    public List<string> ShipTypes { get; set; } = [];
    public List<ShipyardShipDto> ShipyardShips { get; set; } = [];
    public List<ShipyardTransactionDto> ShipyardTransactions { get; set; } = [];
}
