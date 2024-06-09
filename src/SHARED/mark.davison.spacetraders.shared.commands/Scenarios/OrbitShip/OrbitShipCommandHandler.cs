namespace mark.davison.spacetraders.shared.commands.Scenarios.OrbitShip;

public sealed class OrbitShipCommandHandler(
    ICommandProcessor<OrbitShipCommandRequest, OrbitShipCommandResponse> processor
) : ValidateAndProcessCommandHandler<OrbitShipCommandRequest, OrbitShipCommandResponse>(
    processor)
{
}
