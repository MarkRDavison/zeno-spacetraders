namespace mark.davison.spacetraders.shared.commands.Scenarios.ModifyOrbit;

public sealed class ModifyOrbitCommandHandler(
    ICommandProcessor<ModifyOrbitCommandRequest, ModifyOrbitCommandResponse> processor
) : ValidateAndProcessCommandHandler<ModifyOrbitCommandRequest, ModifyOrbitCommandResponse>(
    processor)
{
}
