namespace mark.davison.spacetraders.shared.commands.Scenarios.FetchShips;

public sealed class FetchShipsCommandProcessor : ICommandProcessor<FetchShipsCommandRequest, FetchShipsCommandResponse>
{
    private readonly ISpacetradersDbContext _dbContext;
    private readonly ISpaceTradersApiClient _apiClient;

    public FetchShipsCommandProcessor(
        ISpacetradersDbContext dbContext,
        ISpaceTradersApiClient apiClient)
    {
        _dbContext = dbContext;
        _apiClient = apiClient;
    }

    public async Task<FetchShipsCommandResponse> ProcessAsync(FetchShipsCommandRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        var account = await _dbContext.GetByIdAsync<Account>(request.AccountId, cancellationToken);

        if (account == null)
        {
            return ValidationMessages.CreateErrorResponse<FetchShipsCommandResponse>(
                ValidationMessages.INVALID_PROPERTY,
                nameof(FetchShipsCommandRequest.AccountId));
        }

        _apiClient.Token = account.Token;

        var meta = request.Meta ?? new();

        var apiResponse = await _apiClient.GetMyShipsAsync(meta.Page, meta.Limit, cancellationToken);

        return new FetchShipsCommandResponse
        {
            Value = [.. apiResponse.Data.Select(ShipHelpers.ToShipDto)],
            Meta = new MetaInfo
            {
                Limit = apiResponse.Meta.Limit,
                Page = apiResponse.Meta.Page,
                Total = apiResponse.Meta.Total
            }
        };
    }

}
