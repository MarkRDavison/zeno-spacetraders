namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchShips;

public sealed class FetchShipsQueryHandler(
    IQueryProcessor<FetchShipsQueryRequest, FetchShipsQueryResponse> processor
) : ValidateAndProcessQueryHandler<FetchShipsQueryRequest, FetchShipsQueryResponse>(
    processor)
{
}
