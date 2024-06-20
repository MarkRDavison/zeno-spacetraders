namespace mark.davison.spacetraders.shared.commands.Scenarios.DeliverContractCargo;

public sealed class DeliverContractCargoCommandHandler(
    ICommandProcessor<DeliverContractCargoCommandRequest, DeliverContractCargoCommandResponse> processor
) : ValidateAndProcessCommandHandler<DeliverContractCargoCommandRequest, DeliverContractCargoCommandResponse>(
    processor)
{
}
