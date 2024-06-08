namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchAccounts;

public sealed class FetchAccountsQueryHandler : ValidateAndProcessQueryHandler<FetchAccountsQueryRequest, FetchAccountsQueryResponse>
{
    public FetchAccountsQueryHandler(
        IQueryProcessor<FetchAccountsQueryRequest, FetchAccountsQueryResponse> processor
    ) : base(
        processor)
    {
    }
}
