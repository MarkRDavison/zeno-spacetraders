namespace mark.davison.spacetraders.shared.models.dtos.Commands.ModifyOrbit;

[PostRequest(Path = "mody-orbit-command")]
public sealed class ModifyOrbitCommandRequest : IdentifiedCommandRequest<ModifyOrbitCommandRequest, ModifyOrbitCommandResponse>
{
    public string ShipSymbol { get; set; } = string.Empty;
    public bool Dock { get; set; }
}
