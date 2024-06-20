namespace mark.davison.spacetraders.shared.models.dtos.Queries.FetchContract;

[GetRequest(Path = "fetch-contract-query")]
public sealed class FetchContractQueryRequest : IdentifiedQueryRequest<FetchContractQueryRequest, FetchContractQueryResponse>
{
    public string ContractId { get; set; } = string.Empty;
}