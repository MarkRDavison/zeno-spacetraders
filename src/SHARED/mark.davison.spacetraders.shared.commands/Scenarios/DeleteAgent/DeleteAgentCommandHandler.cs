namespace mark.davison.spacetraders.shared.commands.Scenarios.DeleteAgent;

public sealed class DeleteAgentCommandHandler(
    ICommandProcessor<DeleteAgentCommandRequest, DeleteAgentCommandResponse> processor
) : ValidateAndProcessCommandHandler<DeleteAgentCommandRequest, DeleteAgentCommandResponse>(
    processor)
{
}
