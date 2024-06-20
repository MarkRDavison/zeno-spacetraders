namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchMarketplace;

public sealed class FetchMarketplaceQueryHandler(
    IQueryProcessor<FetchMarketplaceQueryRequest, FetchMarketplaceQueryResponse> processor
) : ValidateAndProcessQueryHandler<FetchMarketplaceQueryRequest, FetchMarketplaceQueryResponse>(
    processor)
{
}
