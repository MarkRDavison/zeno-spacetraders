namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchAccounts;

public sealed class FetchAccountsQueryProcessor : IQueryProcessor<FetchAccountsQueryRequest, FetchAccountsQueryResponse>
{
    private readonly ISpacetradersDbContext _dbContext;

    public FetchAccountsQueryProcessor(ISpacetradersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<FetchAccountsQueryResponse> ProcessAsync(FetchAccountsQueryRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        var accounts = await _dbContext
            .Set<Account>()
            .AsNoTracking()
            .Where(_ => _.UserId == currentUserContext.CurrentUser.Id)
            .ToListAsync(cancellationToken);

        return new FetchAccountsQueryResponse
        {
            Value = [..accounts.Select(_ => new AccountDto
            {
                Id = _.Id,
                Identifier = _.Identifier,
                Version = _.Version,
                Email = _.Email
            })]
        };
    }
}
