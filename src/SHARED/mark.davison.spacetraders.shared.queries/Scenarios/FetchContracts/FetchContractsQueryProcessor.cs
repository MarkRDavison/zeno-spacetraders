namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchContracts;

public sealed class FetchContractsQueryProcessor(
    ISpacetradersDbContext dbContext,
    ISpacetradersApiClient apiClient
) : IdentifiedQueryProcessor<FetchContractsQueryRequest, FetchContractsQueryResponse>(
    dbContext,
    apiClient)
{
    protected override async Task<FetchContractsQueryResponse> ProcessAsyncIdentified(FetchContractsQueryRequest request, ISpacetradersApiClient apiClient, Guid userId, CancellationToken cancellationToken)
    {
        // TODO: All
        var response = await apiClient.GetContractsAsync(request.Page, request.Limit, cancellationToken);

        return new FetchContractsQueryResponse
        {
            Value = [.. response.Data.Select(ContractAdapters.Adapt)]
        };
    }
}
