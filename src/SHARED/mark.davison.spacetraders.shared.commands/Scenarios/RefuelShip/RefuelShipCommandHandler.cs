namespace mark.davison.spacetraders.shared.commands.Scenarios.RefuelShip;

public sealed class RefuelShipCommandHandler(
    ICommandProcessor<RefuelShipCommandRequest, RefuelShipCommandResponse> processor
) : ValidateAndProcessCommandHandler<RefuelShipCommandRequest, RefuelShipCommandResponse>(
    processor)
{
}
