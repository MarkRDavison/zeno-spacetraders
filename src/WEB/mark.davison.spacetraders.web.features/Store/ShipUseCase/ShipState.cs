namespace mark.davison.spacetraders.web.features.Store.ShipUseCase;

[FeatureState]
public sealed class ShipState
{
    public bool IsLoading { get; }
    public ReadOnlyCollection<ShipDto> Ships { get; }

    public ShipState() : this(false, [])
    {

    }

    public ShipState(bool isLoading, IEnumerable<ShipDto> ships)
    {
        IsLoading = isLoading;
        Ships = new ReadOnlyCollection<ShipDto>(ships.ToList());
    }
}
