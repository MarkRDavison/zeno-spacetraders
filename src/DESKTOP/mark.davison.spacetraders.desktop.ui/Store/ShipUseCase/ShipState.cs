namespace mark.davison.spacetraders.desktop.ui.Store.ShipUseCase;

public sealed class ShipState : IDesktopState
{
    public ShipState() : this(false, [], [])
    {

    }

    public ShipState(
        bool loading,
        IEnumerable<ShipDto> ships,
        IEnumerable<ShipNavDto> shipNavs)
    {
        Loading = loading;
        Ships = new([.. ships]);
        ShipNavs = new([.. shipNavs]);
    }

    public bool Loading { get; }

    public ReadOnlyCollection<ShipDto> Ships { get; }
    public ReadOnlyCollection<ShipNavDto> ShipNavs { get; }
}
