namespace mark.davison.spacetraders.shared.models.dtos.Commands.ExtractResources;

[PostRequest(Path = "extract-resources-command")]
public sealed class ExtractResourcesCommandRequest : ICommand<ExtractResourcesCommandRequest, ExtractResourcesCommandResponse>
{
    public Guid AccountId { get; set; }
    public string ShipSymbol { get; set; } = string.Empty;
}
