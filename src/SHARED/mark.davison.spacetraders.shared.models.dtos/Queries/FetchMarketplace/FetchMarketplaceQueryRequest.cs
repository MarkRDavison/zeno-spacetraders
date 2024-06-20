namespace mark.davison.spacetraders.shared.models.dtos.Queries.FetchMarketplace;

[GetRequest(Path = "fetch-marketplace-query")]
public sealed class FetchMarketplaceQueryRequest : IdentifiedQueryRequest<FetchMarketplaceQueryRequest, FetchMarketplaceQueryResponse>
{
    public string SystemSymbol { get; set; } = string.Empty;
    public string WaypointSymbol { get; set; } = string.Empty;
}
