namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchShipyard;

public sealed class FetchShipyardQueryHandler(
    IQueryProcessor<FetchShipyardQueryRequest, FetchShipyardQueryResponse> processor
) : ValidateAndProcessQueryHandler<FetchShipyardQueryRequest, FetchShipyardQueryResponse>(
    processor)
{
}
