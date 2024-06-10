namespace mark.davison.spacetraders.shared.commands.Scenarios.ExtractResources;

public sealed class ExtractResourcesCommandHandler(
    ICommandProcessor<ExtractResourcesCommandRequest, ExtractResourcesCommandResponse> processor
) : ValidateAndProcessCommandHandler<ExtractResourcesCommandRequest, ExtractResourcesCommandResponse>(
    processor)
{
}
