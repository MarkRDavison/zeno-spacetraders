namespace mark.davison.spacetraders.shared.models.dtos.Commands.ExtractResources;

[PostRequest(Path = "extract-resources-command")]
public sealed class ExtractResourcesCommandRequest : IdentifiedCommandRequest<ExtractResourcesCommandRequest, ExtractResourcesCommandResponse>
{
    public string ShipSymbol { get; set; } = string.Empty;
}
