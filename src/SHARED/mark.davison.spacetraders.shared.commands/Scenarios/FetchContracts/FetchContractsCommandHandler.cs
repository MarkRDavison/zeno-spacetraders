namespace mark.davison.spacetraders.shared.commands.Scenarios.FetchContracts;

public sealed class FetchContractsCommandHandler : ValidateAndProcessCommandHandler<FetchContractsCommandRequest, FetchContractsCommandResponse>
{
    public FetchContractsCommandHandler(
        ICommandProcessor<FetchContractsCommandRequest, FetchContractsCommandResponse> processor
    ) : base(
        processor)
    {
    }
}
