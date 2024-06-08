namespace mark.davison.spacetraders.shared.models.dtos.Queries.FetchAccounts;

[GetRequest(Path = "fetch-accounts-query")]
public sealed class FetchAccountsQueryRequest : IQuery<FetchAccountsQueryRequest, FetchAccountsQueryResponse>
{

}
