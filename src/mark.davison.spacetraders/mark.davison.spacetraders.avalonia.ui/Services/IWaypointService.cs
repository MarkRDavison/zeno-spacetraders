namespace mark.davison.spacetraders.avalonia.ui.Services;

public interface IWaypointService
{
    Task<WaypointResponse> GetSystemWaypointsAsync(string systemSymbol, WaypointTraitSymbol? traits, CancellationToken cancellationToken);
    Task<WaypointResponse> GetSystemWaypointsAsync(int page, int limit, string systemSymbol, WaypointTraitSymbol? traits, CancellationToken cancellationToken);
}
