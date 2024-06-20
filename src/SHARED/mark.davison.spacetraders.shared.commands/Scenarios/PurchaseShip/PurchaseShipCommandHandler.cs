namespace mark.davison.spacetraders.shared.commands.Scenarios.PurchaseShip;

public sealed class PurchaseShipCommandHandler(
    ICommandProcessor<PurchaseShipCommandRequest, PurchaseShipCommandResponse> processor
) : ValidateAndProcessCommandHandler<PurchaseShipCommandRequest, PurchaseShipCommandResponse>(
    processor)
{
}
