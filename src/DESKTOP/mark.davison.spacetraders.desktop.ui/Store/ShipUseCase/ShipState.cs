namespace mark.davison.spacetraders.desktop.ui.Store.ShipUseCase;

public sealed class ShipState : IDesktopState
{
    public ShipState() : this(false, [])
    {

    }

    public ShipState(bool loading, IEnumerable<ShipDto> ships)
    {
        Loading = loading;
        Ships = new([.. ships]);
    }

    public bool Loading { get; }

    public ReadOnlyCollection<ShipDto> Ships { get; }
}
