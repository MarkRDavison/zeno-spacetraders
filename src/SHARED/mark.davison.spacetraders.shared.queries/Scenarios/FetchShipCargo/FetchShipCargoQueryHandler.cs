namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchShipCargo;

public sealed class FetchShipCargoQueryHandler(
    IQueryProcessor<FetchShipCargoQueryRequest, FetchShipCargoQueryResponse> processor
) : ValidateAndProcessQueryHandler<FetchShipCargoQueryRequest, FetchShipCargoQueryResponse>(
    processor)
{
}
