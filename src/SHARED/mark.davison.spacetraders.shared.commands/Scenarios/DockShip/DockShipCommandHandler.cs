namespace mark.davison.spacetraders.shared.commands.Scenarios.DockShip;

public sealed class DockShipCommandHandler(
    ICommandProcessor<DockShipCommandRequest, DockShipCommandResponse> processor
) : ValidateAndProcessCommandHandler<DockShipCommandRequest, DockShipCommandResponse>(
    processor)
{
}
