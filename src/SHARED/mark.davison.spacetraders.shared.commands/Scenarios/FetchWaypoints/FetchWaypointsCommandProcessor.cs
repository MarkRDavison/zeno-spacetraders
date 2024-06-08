namespace mark.davison.spacetraders.shared.commands.Scenarios.FetchWaypoints;

public sealed class FetchWaypointsCommandProcessor : ICommandProcessor<FetchWaypointsCommandRequest, FetchWaypointsCommandResponse>
{
    private readonly ISpacetradersDbContext _dbContext;
    private readonly ISpaceTradersApiClient _apiClient;

    public FetchWaypointsCommandProcessor(
        ISpacetradersDbContext dbContext,
        ISpaceTradersApiClient apiClient)
    {
        _dbContext = dbContext;
        _apiClient = apiClient;
    }

    public async Task<FetchWaypointsCommandResponse> ProcessAsync(FetchWaypointsCommandRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        var account = await _dbContext.GetByIdAsync<Account>(request.AccountId, cancellationToken);

        if (account == null)
        {
            return ValidationMessages.CreateErrorResponse<FetchWaypointsCommandResponse>(
                ValidationMessages.INVALID_PROPERTY,
                nameof(FetchWaypointsCommandRequest.AccountId));
        }

        _apiClient.Token = account.Token;

        var meta = request.Meta ?? new();

        WaypointTraitSymbol? traitToSearch = null;

        if (Enum.TryParse<WaypointTraitSymbol>(request.Trait, out var te))
        {
            traitToSearch = te;
        }

        var apiResponse = await _apiClient.GetSystemWaypointsAsync(
            meta.Page,
            meta.Limit,
            null,
            traitToSearch,
            request.SystemSymbol,
            cancellationToken);

        return new FetchWaypointsCommandResponse
        {
            Value = [.. apiResponse.Data.Select(FromWaypoint)],
            Meta = new MetaInfo
            {
                Limit = apiResponse.Meta.Limit,
                Page = apiResponse.Meta.Page,
                Total = apiResponse.Meta.Total
            }
        };
    }

    private WaypointDto FromWaypoint(Waypoint waypoint)
    {
        return new WaypointDto
        {
            WaypointSymbol = waypoint.Symbol,
            SystemSymbol = waypoint.SystemSymbol,
            Type = waypoint.Type.ToString(),
            Traits = [.. waypoint.Traits.Select(_ => _.Name)]
        };
    }
}
