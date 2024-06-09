namespace mark.davison.spacetraders.shared.commands.Scenarios.FetchShip;

public sealed class FetchShipCommandHandler(
    ICommandProcessor<FetchShipCommandRequest, FetchShipCommandResponse> processor
    ) : ValidateAndProcessCommandHandler<FetchShipCommandRequest, FetchShipCommandResponse>(
        processor)
{
}
