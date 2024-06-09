namespace mark.davison.spacetraders.shared.commands.Scenarios.AcceptContract;

public sealed class AcceptContractCommandHandler(
    ICommandProcessor<AcceptContractCommandRequest, AcceptContractCommandResponse> processor
) : ValidateAndProcessCommandHandler<AcceptContractCommandRequest, AcceptContractCommandResponse>(
    processor)
{
}
