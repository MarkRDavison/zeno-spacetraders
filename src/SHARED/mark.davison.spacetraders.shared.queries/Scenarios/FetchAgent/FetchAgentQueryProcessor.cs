namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchAgent;

public sealed class FetchAgentQueryProcessor(
    ISpacetradersDbContext dbContext,
    ISpacetradersApiClient apiClient
) : IdentifiedQueryProcessor<FetchAgentQueryRequest, FetchAgentQueryResponse>(
    dbContext,
    apiClient)
{
    protected override async Task<FetchAgentQueryResponse> ProcessAsyncIdentified(FetchAgentQueryRequest request, ISpacetradersApiClient apiClient, Guid userId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.AgentSymbol))
        {
            var response = await apiClient.GetMyAgentAsync(cancellationToken);

            return new FetchAgentQueryResponse
            {
                Value = new AgentDto
                {
                    Symbol = response.Data.Symbol,
                    Credits = response.Data.Credits,
                    Headquarters = response.Data.Headquarters,
                    ShipCount = response.Data.ShipCount
                }
            };
        }
        else
        {
            var response = await apiClient.GetAgentAsync(request.AgentSymbol, cancellationToken);

            return new FetchAgentQueryResponse
            {
                Value = new AgentDto
                {
                    Symbol = response.Data.Symbol,
                    Credits = response.Data.Credits,
                    Headquarters = response.Data.Headquarters,
                    ShipCount = response.Data.ShipCount
                }
            };
        }
    }
}
