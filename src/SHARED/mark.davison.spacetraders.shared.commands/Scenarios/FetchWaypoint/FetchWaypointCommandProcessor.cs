namespace mark.davison.spacetraders.shared.commands.Scenarios.FetchWaypoint;

public sealed class FetchWaypointCommandProcessor : ICommandProcessor<FetchWaypointCommandRequest, FetchWaypointCommandResponse>
{
    private readonly ISpacetradersDbContext _dbContext;
    private readonly ISpaceTradersApiClient _apiClient;

    public FetchWaypointCommandProcessor(
        ISpacetradersDbContext dbContext,
        ISpaceTradersApiClient apiClient)
    {
        _dbContext = dbContext;
        _apiClient = apiClient;
    }

    public async Task<FetchWaypointCommandResponse> ProcessAsync(FetchWaypointCommandRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        var account = await _dbContext.GetByIdAsync<Account>(request.AccountId, cancellationToken);

        if (account == null)
        {
            return ValidationMessages.CreateErrorResponse<FetchWaypointCommandResponse>(
                ValidationMessages.INVALID_PROPERTY,
                nameof(FetchWaypointCommandRequest.AccountId));
        }

        _apiClient.Token = account.Token;

        var apiResponse = await _apiClient.GetWaypointAsync(
            WaypointsHelpers.GetSystemFromWaypoint(request.WaypointSymbol),
            request.WaypointSymbol,
            cancellationToken);

        return new FetchWaypointCommandResponse
        {
            Value = WaypointHelpers.FromWaypoint(apiResponse.Data)
        };
    }
}
