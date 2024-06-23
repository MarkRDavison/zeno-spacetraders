using System.Linq;

namespace mark.davison.spacetraders.desktop.ui.ViewModels;

public partial class ShipsPageViewModel : MainApplicationPageViewModel
{
    private readonly IStoreHelper _storeHelper;

    public IState<ShipState> ShipState { get; }

    public ShipsPageViewModel(
        IApplicationNotificationService applicationNotificationService,
        IAccountService accountService,
        ILogger<ShipsPageViewModel> logger,
        IStoreHelper storeHelper,
        IState<ShipState> shipState
    ) : base(
        applicationNotificationService,
        accountService,
        logger)
    {
        _storeHelper = storeHelper;
        ShipState = shipState;
    }

    // TODO: Replace with async next bump
    protected override void OnSelected(bool firstTime)
    {
        if (!ShipState.Value.Ships.Any())
        {
            _ = LoadAllShipsAsync();
        }
    }

    private async Task LoadAllShipsAsync()
    {
        await _storeHelper.DispatchAndWaitForResponse<
            FetchShipsAction,
            UpdateShipsActionResponse>(
            new());
    }

    [ObservableProperty]
    private ShipDto? _selectedItem;

    public override string Name => "Ships";
}
