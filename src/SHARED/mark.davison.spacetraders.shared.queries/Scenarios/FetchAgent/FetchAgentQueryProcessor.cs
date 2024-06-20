namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchAgent;

public sealed class FetchAgentQueryProcessor(
    ISpacetradersDbContext dbContext,
    ISpaceTradersApiClient apiClient
) : IdentifiedQueryProcessor<FetchAgentQueryRequest, FetchAgentQueryResponse>(
    dbContext,
    apiClient)
{
    protected override async Task<FetchAgentQueryResponse> ProcessAsyncIdentified(FetchAgentQueryRequest request, ISpaceTradersApiClient apiClient, Guid userId, CancellationToken cancellationToken)
    {
        var apiResponse = await apiClient.GetMyAgentAsync(cancellationToken);

        return new FetchAgentQueryResponse
        {
            Value = AgentHelpers.ToAgentDto(apiResponse.Data)
        };
    }
}
