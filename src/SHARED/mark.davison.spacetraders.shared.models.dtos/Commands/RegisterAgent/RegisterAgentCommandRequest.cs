namespace mark.davison.spacetraders.shared.models.dtos.Commands.RegisterAgent;

[PostRequest(Path = "register-agent-command")]
public sealed class RegisterAgentCommandRequest : ICommand<RegisterAgentCommandRequest, RegisterAgentCommandResponse>
{
    public string Identifier { get; set; } = string.Empty;
    public string Faction { get; set; } = string.Empty;
    public string? Email { get; set; }
}
