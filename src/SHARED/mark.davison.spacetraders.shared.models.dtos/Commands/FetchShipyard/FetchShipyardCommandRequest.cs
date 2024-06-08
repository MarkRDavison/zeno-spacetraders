namespace mark.davison.spacetraders.shared.models.dtos.Commands.FetchShipyard;

[PostRequest(Path = "fetch-shipyard-command")]
public sealed class FetchShipyardCommandRequest : ICommand<FetchShipyardCommandRequest, FetchShipyardCommandResponse>
{
    public Guid AccountId { get; set; }
    public string SystemSymbol { get; set; } = string.Empty;
    public string WaypointSymbol { get; set; } = string.Empty;
}
