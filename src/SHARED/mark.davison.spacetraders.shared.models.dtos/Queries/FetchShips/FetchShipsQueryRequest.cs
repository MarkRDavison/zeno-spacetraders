namespace mark.davison.spacetraders.shared.models.dtos.Queries.FetchShips;

[GetRequest(Path = "fetch-ships-query")]
public sealed class FetchShipsQueryRequest : PaginatedIdentifiedQueryRequest<FetchShipsQueryRequest, FetchShipsQueryResponse>
{

}
