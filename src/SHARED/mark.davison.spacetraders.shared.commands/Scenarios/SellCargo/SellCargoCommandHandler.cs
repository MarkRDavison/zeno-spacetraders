namespace mark.davison.spacetraders.shared.commands.Scenarios.SellCargo;

public sealed class SellCargoCommandHandler(
    ICommandProcessor<SellCargoCommandRequest, SellCargoCommandResponse> processor
) : ValidateAndProcessCommandHandler<SellCargoCommandRequest, SellCargoCommandResponse>(
    processor)
{
}
