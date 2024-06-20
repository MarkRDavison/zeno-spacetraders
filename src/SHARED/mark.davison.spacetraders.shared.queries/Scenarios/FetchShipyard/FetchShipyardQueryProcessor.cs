namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchShipyard;

public sealed class FetchShipyardQueryProcessor(
    ISpacetradersDbContext dbContext,
    ISpaceTradersApiClient apiClient
) : IdentifiedQueryProcessor<FetchShipyardQueryRequest, FetchShipyardQueryResponse>(
    dbContext,
    apiClient)
{
    protected override async Task<FetchShipyardQueryResponse> ProcessAsyncIdentified(FetchShipyardQueryRequest request, ISpaceTradersApiClient apiClient, Guid userId, CancellationToken cancellationToken)
    {
        var apiResponse = await apiClient.GetShipyardAsync(
            request.SystemSymbol,
            request.WaypointSymbol,
            cancellationToken);

        return new FetchShipyardQueryResponse
        {
            Value = WaypointHelpers.ToWaypointResponse(apiResponse.Data)
        };
    }
}
