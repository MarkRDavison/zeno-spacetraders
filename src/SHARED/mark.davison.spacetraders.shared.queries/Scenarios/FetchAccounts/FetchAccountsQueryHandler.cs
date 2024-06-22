namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchAccounts;

public sealed class FetchAccountsQueryHandler(
    IQueryProcessor<FetchAccountsQueryRequest, FetchAccountsQueryResponse> processor
) : ValidateAndProcessQueryHandler<FetchAccountsQueryRequest, FetchAccountsQueryResponse>(
    processor)
{
}
