namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchContracts;

public sealed class FetchContractsQueryProcessor(
    ISpacetradersDbContext dbContext,
    ISpaceTradersApiClient apiClient
) : IdentifiedQueryProcessor<FetchContractsQueryRequest, FetchContractsQueryResponse>(
    dbContext,
    apiClient)
{
    protected override async Task<FetchContractsQueryResponse> ProcessAsyncIdentified(FetchContractsQueryRequest request, ISpaceTradersApiClient apiClient, Guid userId, CancellationToken cancellationToken)
    {
        // TODO: pagination options in query, another base class? PaginatedIdentifiedQueryRequest?
        var apiResponse = await apiClient.GetContractsAsync(null, 20, cancellationToken);

        return new FetchContractsQueryResponse
        {
            Value = [.. apiResponse.Data.Select(ContractHelpers.ToContractDto)]
        };
    }
}
