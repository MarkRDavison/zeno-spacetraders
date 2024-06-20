namespace mark.davison.spacetraders.shared.models.dtos.Queries.FetchContracts;

[GetRequest(Path = "fetch-contracts-query")]
public sealed class FetchContractsQueryRequest : IdentifiedQueryRequest<FetchContractsQueryRequest, FetchContractsQueryResponse>
{
}
