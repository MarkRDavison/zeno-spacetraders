namespace mark.davison.spacetraders.shared.commands.Scenarios.FetchWaypoint;

public sealed class FetchWaypointCommandHandler(
    ICommandProcessor<FetchWaypointCommandRequest, FetchWaypointCommandResponse> processor
) : ValidateAndProcessCommandHandler<FetchWaypointCommandRequest, FetchWaypointCommandResponse>(
    processor)
{
}
