namespace mark.davison.spacetraders.shared.models.dtos.Commands.FetchShips;

public sealed class FetchShipsCommandResponse : Response<List<ShipDto>>
{
    public MetaInfo Meta { get; set; } = new();
}
