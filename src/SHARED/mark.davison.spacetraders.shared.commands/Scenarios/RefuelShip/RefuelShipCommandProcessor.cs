namespace mark.davison.spacetraders.shared.commands.Scenarios.RefuelShip;

public sealed class RefuelShipCommandProcessor : ICommandProcessor<RefuelShipCommandRequest, RefuelShipCommandResponse>
{
    private readonly ISpacetradersDbContext _dbContext;
    private readonly ISpaceTradersApiClient _apiClient;

    public RefuelShipCommandProcessor(
        ISpacetradersDbContext dbContext,
        ISpaceTradersApiClient apiClient)
    {
        _dbContext = dbContext;
        _apiClient = apiClient;
    }

    public async Task<RefuelShipCommandResponse> ProcessAsync(RefuelShipCommandRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        var account = await _dbContext.GetByIdAsync<Account>(request.AccountId, cancellationToken);

        if (account == null)
        {
            return ValidationMessages.CreateErrorResponse<RefuelShipCommandResponse>(
                ValidationMessages.INVALID_PROPERTY,
                nameof(RefuelShipCommandRequest.AccountId));
        }

        _apiClient.Token = account.Token;

        var apiResponse = await _apiClient.RefuelShipAsync(
            new()
            {
                FromCargo = request.FromCargo,
                Units = request.Units
            },
            request.ShipSymbol,
            cancellationToken);

        return new RefuelShipCommandResponse
        {
            Value = ShipHelpers.ToShipFuelDto(apiResponse.Data.Fuel),
            Credits = apiResponse.Data.Agent.Credits
        };
    }
}
