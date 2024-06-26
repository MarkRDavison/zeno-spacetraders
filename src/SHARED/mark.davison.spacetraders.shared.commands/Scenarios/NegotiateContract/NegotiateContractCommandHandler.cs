namespace mark.davison.spacetraders.shared.commands.Scenarios.NegotiateContract;

public sealed class NegotiateContractCommandHandler(
    ICommandProcessor<NegotiateContractCommandRequest, NegotiateContractCommandResponse> processor
) : ValidateAndProcessCommandHandler<NegotiateContractCommandRequest, NegotiateContractCommandResponse>(
    processor)
{
}
