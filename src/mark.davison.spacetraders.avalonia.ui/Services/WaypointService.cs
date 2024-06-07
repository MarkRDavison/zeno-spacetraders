namespace mark.davison.spacetraders.avalonia.ui.Services;

public sealed class WaypointService : IWaypointService
{
    private readonly ISpaceTradersApiClient _apiClient;

    public WaypointService(ISpaceTradersApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<WaypointResponse> GetSystemWaypointsAsync(string systemSymbol, WaypointTraitSymbol? traits, CancellationToken cancellationToken)
    {
        return await GetSystemWaypointsAsync(null, null, systemSymbol, null, null, cancellationToken);
    }
    public async Task<WaypointResponse> GetSystemWaypointsAsync(int page, int limit, string systemSymbol, WaypointTraitSymbol? traits, CancellationToken cancellationToken)
    {
        return await GetSystemWaypointsAsync(page, limit, systemSymbol, null, traits, cancellationToken);
    }

    private async Task<WaypointResponse> GetSystemWaypointsAsync(
        int? page,
        int? limit,
        string systemSymbol,
        WaypointType? type,
        WaypointTraitSymbol? traits,
        CancellationToken cancellationToken)
    {
        if (limit != null && limit > 20)
        {
            limit = 20;
        }

        var response = await _apiClient.GetSystemWaypointsAsync(page, limit, type, traits, systemSymbol, cancellationToken);

        return new WaypointResponse(response.Meta, response.Data);
    }

}
