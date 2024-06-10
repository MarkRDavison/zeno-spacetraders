namespace mark.davison.spacetraders.shared.commands.Scenarios.NavigateShip;

public sealed class NavigateShipCommandHandler(
    ICommandProcessor<NavigateShipCommandRequest, NavigateShipCommandResponse> processor
) : ValidateAndProcessCommandHandler<NavigateShipCommandRequest, NavigateShipCommandResponse>(
    processor)
{
}
