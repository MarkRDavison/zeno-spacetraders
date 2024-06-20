namespace mark.davison.spacetraders.shared.commands.Scenarios.JettisonCargo;

public sealed class JettisonCargoCommandHandler(
    ICommandProcessor<JettisonCargoCommandRequest, JettisonCargoCommandResponse> processor
) : ValidateAndProcessCommandHandler<JettisonCargoCommandRequest, JettisonCargoCommandResponse>(
    processor)
{
}
