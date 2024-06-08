namespace mark.davison.spacetraders.shared.models.dtos.Commands.FetchShips;

[PostRequest(Path = "fetch-ships-command")]
public sealed class FetchShipsCommandRequest : ICommand<FetchShipsCommandRequest, FetchShipsCommandResponse>
{
    public Guid AccountId { get; set; }
    public MetaInfo? Meta { get; set; }
}
