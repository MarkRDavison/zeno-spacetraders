namespace mark.davison.spacetraders.web.features.Store.ShipUseCase;

[FeatureState]
public sealed class ShipState
{
    public bool IsLoading { get; }
    public ReadOnlyCollection<ShipDto> Ships { get; }
    public ReadOnlyCollection<ShipCargoDto> Cargo { get; }
    public ShipState() : this(false, [], [])
    {

    }

    public ShipState(bool isLoading, IEnumerable<ShipDto> ships, IEnumerable<ShipCargoDto> cargo)
    {
        IsLoading = isLoading;
        Ships = new ReadOnlyCollection<ShipDto>(ships.ToList());
        Cargo = new ReadOnlyCollection<ShipCargoDto>(cargo.ToList());
    }
}
