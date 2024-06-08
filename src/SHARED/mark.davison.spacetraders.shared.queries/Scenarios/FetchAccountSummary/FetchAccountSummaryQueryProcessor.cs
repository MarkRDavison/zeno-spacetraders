namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchAccountSummary;

public sealed class FetchAccountSummaryQueryProcessor : IQueryProcessor<FetchAccountSummaryQueryRequest, FetchAccountSummaryQueryResponse>
{
    private readonly ISpacetradersDbContext _dbContext;
    private readonly ISpaceTradersApiClient _apiClient;

    public FetchAccountSummaryQueryProcessor(
        ISpacetradersDbContext dbContext,
        ISpaceTradersApiClient apiClient)
    {
        _dbContext = dbContext;
        _apiClient = apiClient;
    }

    public async Task<FetchAccountSummaryQueryResponse> ProcessAsync(FetchAccountSummaryQueryRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        var account = await _dbContext.GetByIdAsync<Account>(request.AccountId, cancellationToken);

        if (account == null)
        {
            return ValidationMessages.CreateErrorResponse<FetchAccountSummaryQueryResponse>(
                ValidationMessages.INVALID_PROPERTY,
                nameof(FetchAccountSummaryQueryRequest.AccountId));
        }

        _apiClient.Token = account.Token;

        var apiResponse = await _apiClient.GetMyAgentAsync();

        return new FetchAccountSummaryQueryResponse
        {
            Value = new AccountSummaryDto
            {
                AccountId = request.AccountId,
                Credits = apiResponse.Data.Credits,
                Headquarters = apiResponse.Data.Headquarters,
                ShipCount = apiResponse.Data.ShipCount
            }
        };
    }
}
