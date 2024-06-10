namespace mark.davison.spacetraders.shared.commands.Scenarios.ExtractResources;

public sealed class ExtractResourcesCommandProcessor : ICommandProcessor<ExtractResourcesCommandRequest, ExtractResourcesCommandResponse>
{
    private readonly ISpacetradersDbContext _dbContext;
    private readonly ISpaceTradersApiClient _apiClient;

    public ExtractResourcesCommandProcessor(
        ISpacetradersDbContext dbContext,
        ISpaceTradersApiClient apiClient)
    {
        _dbContext = dbContext;
        _apiClient = apiClient;
    }

    public async Task<ExtractResourcesCommandResponse> ProcessAsync(ExtractResourcesCommandRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        var account = await _dbContext.GetByIdAsync<Account>(request.AccountId, cancellationToken);

        if (account == null)
        {
            return ValidationMessages.CreateErrorResponse<ExtractResourcesCommandResponse>(
                ValidationMessages.INVALID_PROPERTY,
                nameof(ExtractResourcesCommandRequest.AccountId));
        }

        _apiClient.Token = account.Token;

        var apiResult = await _apiClient.ExtractResourcesAsync(
            new(),
            request.ShipSymbol,
            cancellationToken);

        return new ExtractResourcesCommandResponse
        {
            // TODO: Update cooldown as well
            Value = ShipHelpers.ToShipCargoDto(apiResult.Data.Cargo)
        };
    }
}
