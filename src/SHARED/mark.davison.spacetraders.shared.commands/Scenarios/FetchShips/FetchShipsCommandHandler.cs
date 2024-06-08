namespace mark.davison.spacetraders.shared.commands.Scenarios.FetchShips;

public sealed class FetchShipsCommandHandler(
    ICommandProcessor<FetchShipsCommandRequest, FetchShipsCommandResponse> processor
        ) : ValidateAndProcessCommandHandler<FetchShipsCommandRequest, FetchShipsCommandResponse>(
            processor)
{
}
