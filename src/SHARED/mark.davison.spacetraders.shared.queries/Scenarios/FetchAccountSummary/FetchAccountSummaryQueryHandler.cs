namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchAccountSummary;

public sealed class FetchAccountSummaryQueryHandler : ValidateAndProcessQueryHandler<FetchAccountSummaryQueryRequest, FetchAccountSummaryQueryResponse>
{
    public FetchAccountSummaryQueryHandler(
        IQueryProcessor<FetchAccountSummaryQueryRequest, FetchAccountSummaryQueryResponse> processor
    ) : base(
        processor)
    {
    }
}
