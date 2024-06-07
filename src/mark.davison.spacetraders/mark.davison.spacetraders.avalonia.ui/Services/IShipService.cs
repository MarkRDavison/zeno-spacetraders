namespace mark.davison.spacetraders.avalonia.ui.Services;

public interface IShipService
{
    Task<List<Ship>> GetShipsAsync(CancellationToken cancellationToken);
    Task<Ship> GetShipAsync(string symbol, CancellationToken cancellationToken);
}
