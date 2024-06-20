namespace mark.davison.spacetraders.web.features.Store.ShipUseCase;

[FeatureState]
public sealed class ShipState
{
    public bool IsLoading { get; }
    public ReadOnlyCollection<ShipDto> Ships { get; }
    public ReadOnlyCollection<ShipNavDto> ShipNavs { get; }
    public ReadOnlyCollection<ShipNavRouteDto> ShipNavRoutes { get; }
    public ReadOnlyCollection<ShipCargoDto> ShipCargo { get; }
    public ReadOnlyCollection<CooldownDto> ShipCooldown { get; }
    public ReadOnlyCollection<ShipFuelDto> ShipFuel { get; }

    public ShipState() : this(false, [], [], [], [], [], [])
    {

    }

    public ShipState(
        bool isLoading,
        IEnumerable<ShipDto> ships,
        IEnumerable<ShipNavDto> shipNavs,
        IEnumerable<ShipNavRouteDto> shipNavRoutes,
        IEnumerable<ShipCargoDto> shipCargo,
        IEnumerable<CooldownDto> shipCooldowns,
        IEnumerable<ShipFuelDto> shipFuels)
    {
        IsLoading = isLoading;
        Ships = new(ships.ToList());
        ShipNavs = new(shipNavs.ToList());
        ShipNavRoutes = new(shipNavRoutes.ToList());
        ShipCargo = new(shipCargo.ToList());
        ShipCooldown = new(shipCooldowns.ToList());
        ShipFuel = new(shipFuels.ToList());
    }

    public ShipDto? GetShip(string symbol) => Ships.FirstOrDefault(_ => _.ShipSymbol == symbol);
    public ShipNavDto? GetShipNav(string symbol) => ShipNavs.FirstOrDefault(_ => _.ShipSymbol == symbol);
    public ShipNavRouteDto? GetShipNavRoute(string symbol) => ShipNavRoutes.FirstOrDefault(_ => _.ShipSymbol == symbol);
    public ShipCargoDto? GetShipCargo(string symbol) => ShipCargo.FirstOrDefault(_ => _.ShipSymbol == symbol);
    public CooldownDto? GetShipCooldown(string symbol) => ShipCooldown.FirstOrDefault(_ => _.ShipSymbol == symbol);
    public ShipFuelDto? GetShipFuel(string symbol) => ShipFuel.FirstOrDefault(_ => _.ShipSymbol == symbol);
}