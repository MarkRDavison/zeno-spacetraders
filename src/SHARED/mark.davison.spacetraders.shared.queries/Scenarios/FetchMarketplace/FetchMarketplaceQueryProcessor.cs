namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchMarketplace;

public sealed class FetchMarketplaceQueryProcessor(
    ISpacetradersDbContext dbContext,
    ISpaceTradersApiClient apiClient
) : IdentifiedQueryProcessor<FetchMarketplaceQueryRequest, FetchMarketplaceQueryResponse>(
    dbContext,
    apiClient)
{
    protected override async Task<FetchMarketplaceQueryResponse> ProcessAsyncIdentified(FetchMarketplaceQueryRequest request, ISpaceTradersApiClient apiClient, Guid userId, CancellationToken cancellationToken)
    {
        var apiResponse = await apiClient.GetMarketAsync(
            request.SystemSymbol,
            request.WaypointSymbol,
            cancellationToken);

        return new FetchMarketplaceQueryResponse
        {
            Value = WaypointHelpers.ToWaypointResponse(apiResponse.Data)
        };
    }
}
