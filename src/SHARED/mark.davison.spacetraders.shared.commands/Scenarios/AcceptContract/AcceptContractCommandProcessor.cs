namespace mark.davison.spacetraders.shared.commands.Scenarios.AcceptContract;

public sealed class AcceptContractCommandProcessor(
    ISpacetradersDbContext dbContext,
    ISpacetradersApiClient apiClient
) : IdentifiedCommandProcessor<AcceptContractCommandRequest, AcceptContractCommandResponse>(
    dbContext,
    apiClient)
{
    protected override async Task<AcceptContractCommandResponse> ProcessAsyncIdentified(AcceptContractCommandRequest request, ISpacetradersApiClient apiClient, Guid userId, CancellationToken cancellationToken)
    {
        var apiResponse = await apiClient.AcceptContractAsync(request.ContractId, cancellationToken);

        return new AcceptContractCommandResponse
        {
            Value = apiResponse.Data.Contract.Adapt(),
            Agent = apiResponse.Data.Agent.Adapt()
        };
    }
}
