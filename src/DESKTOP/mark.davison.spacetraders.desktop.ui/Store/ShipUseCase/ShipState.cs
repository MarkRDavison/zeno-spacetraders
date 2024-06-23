namespace mark.davison.spacetraders.desktop.ui.Store.ShipUseCase;

public sealed class ShipState : IDesktopState
{
    public ShipState() : this([])
    {

    }

    public ShipState(IEnumerable<ShipDto> ships)
    {
        Ships = new([.. ships]);
    }

    public ReadOnlyCollection<ShipDto> Ships { get; }
}
