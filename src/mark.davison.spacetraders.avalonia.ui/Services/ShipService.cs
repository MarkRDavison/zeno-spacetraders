namespace mark.davison.spacetraders.avalonia.ui.Services;

public sealed class ShipService : IShipService
{
    private readonly ISpaceTradersApiClient _apiClient;

    public ShipService(ISpaceTradersApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<List<Ship>> GetShipsAsync(CancellationToken cancellationToken)
    {
        var response = await _apiClient.GetMyShipsAsync(null, null, cancellationToken);

        return [.. response.Data];
    }

    public async Task<Ship> GetShipAsync(string symbol, CancellationToken cancellationToken)
    {
        var response = await _apiClient.GetMyShipAsync(symbol, cancellationToken);

        return response.Data;
    }
}
