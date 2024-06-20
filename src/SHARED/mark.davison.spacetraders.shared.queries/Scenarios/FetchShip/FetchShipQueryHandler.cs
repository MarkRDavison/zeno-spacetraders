namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchShip;

public sealed class FetchShipQueryHandler(
    IQueryProcessor<FetchShipQueryRequest, FetchShipQueryResponse> processor
) : ValidateAndProcessQueryHandler<FetchShipQueryRequest, FetchShipQueryResponse>(
    processor)
{
}
