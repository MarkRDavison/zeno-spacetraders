namespace mark.davison.spacetraders.shared.commands.Scenarios.ModifyShipState;

public sealed class ModifyShipStateCommandHandler(
    ICommandProcessor<ModifyShipStateCommandRequest, ModifyShipStateCommandResponse> processor
) : ValidateAndProcessCommandHandler<ModifyShipStateCommandRequest, ModifyShipStateCommandResponse>(
    processor)
{
}
