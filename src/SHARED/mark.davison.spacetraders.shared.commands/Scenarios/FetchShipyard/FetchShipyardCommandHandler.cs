namespace mark.davison.spacetraders.shared.commands.Scenarios.FetchShipyard;

public class FetchShipyardCommandHandler(
    ICommandProcessor<FetchShipyardCommandRequest, FetchShipyardCommandResponse> processor
    ) : ValidateAndProcessCommandHandler<FetchShipyardCommandRequest, FetchShipyardCommandResponse>(
        processor)
{
}
