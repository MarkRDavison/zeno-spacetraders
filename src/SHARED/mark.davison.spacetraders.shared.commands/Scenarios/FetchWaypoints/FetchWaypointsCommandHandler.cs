namespace mark.davison.spacetraders.shared.commands.Scenarios.FetchWaypoints;

public sealed class FetchWaypointsCommandHandler(
    ICommandProcessor<FetchWaypointsCommandRequest, FetchWaypointsCommandResponse> processor
    ) : ValidateAndProcessCommandHandler<FetchWaypointsCommandRequest, FetchWaypointsCommandResponse>(
        processor)
{
}
