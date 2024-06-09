namespace mark.davison.spacetraders.shared.models.dtos.Commands.FetchShip;

public sealed class FetchShipCommandRequest : ICommand<FetchShipCommandRequest, FetchShipCommandResponse>
{
    public Guid AccountId { get; set; }
    public string ShipSymbol { get; set; } = string.Empty;
}
