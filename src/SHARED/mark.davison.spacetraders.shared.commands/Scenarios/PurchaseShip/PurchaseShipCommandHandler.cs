namespace mark.davison.spacetraders.shared.commands.Scenarios.PurchaseShip;

public sealed class PurchaseShipCommandHandler(
    ICommandProcessor<PurchaseShipCommandRequest, PurchaseShipCommandResponse> processor,
    ICommandValidator<PurchaseShipCommandRequest, PurchaseShipCommandResponse> validator
) : ValidateAndProcessCommandHandler<PurchaseShipCommandRequest, PurchaseShipCommandResponse>(
    processor,
    validator)
{
}
