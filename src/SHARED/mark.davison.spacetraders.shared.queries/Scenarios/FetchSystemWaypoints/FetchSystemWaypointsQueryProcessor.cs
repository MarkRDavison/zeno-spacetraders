namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchSystemWaypoints;

public sealed class FetchSystemWaypointsQueryProcessor(
    ISpacetradersDbContext dbContext,
    ISpacetradersApiClient apiClient
) : IdentifiedQueryProcessor<FetchSystemWaypointsQueryRequest, FetchSystemWaypointsQueryResponse>(
    dbContext,
    apiClient)
{
    protected override Task<FetchSystemWaypointsQueryResponse> ProcessAsyncIdentified(FetchSystemWaypointsQueryRequest request, ISpacetradersApiClient apiClient, Guid userId, CancellationToken cancellationToken)
    {
        if (request.All)
        {

        }
        else
        {

        }

        throw new NotImplementedException();
    }
}
