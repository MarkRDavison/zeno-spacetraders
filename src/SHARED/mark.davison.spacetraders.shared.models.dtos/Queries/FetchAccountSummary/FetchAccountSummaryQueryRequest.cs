namespace mark.davison.spacetraders.shared.models.dtos.Queries.FetchAccountSummary;

[GetRequest(Path = "fetch-account-summary-query")]
public sealed class FetchAccountSummaryQueryRequest : IQuery<FetchAccountSummaryQueryRequest, FetchAccountSummaryQueryResponse>
{
    public Guid AccountId { get; set; }
}
