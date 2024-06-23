namespace mark.davison.spacetraders.shared.commands.Scenarios.RegisterAgent;

public sealed class RegisterAgentCommandHandler(
    ICommandProcessor<RegisterAgentCommandRequest, RegisterAgentCommandResponse> processor
) : ValidateAndProcessCommandHandler<RegisterAgentCommandRequest, RegisterAgentCommandResponse>(
    processor)
{
}
