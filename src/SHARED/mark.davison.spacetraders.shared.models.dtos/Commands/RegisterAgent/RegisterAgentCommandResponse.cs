namespace mark.davison.spacetraders.shared.models.dtos.Commands.RegisterAgent;

public sealed class RegisterAgentCommandResponse : Response<AgentDto>
{
    public AccountDto Account { get; set; } = new();
}
