namespace mark.davison.spacetraders.desktop.ui.ViewModels;

public partial class ShipsPageViewModel : MainApplicationPageViewModel
{
    private readonly IStoreHelper _storeHelper;

    public IState<ShipState> ShipState { get; }

    public ShipsPageViewModel(
        IApplicationNotificationService applicationNotificationService,
        ICommonApplicationNotificationService commonApplicationNotificationService,
        IAccountService accountService,
        ILogger<ShipsPageViewModel> logger,
        IStoreHelper storeHelper,
        IState<ShipState> shipState
    ) : base(
        applicationNotificationService,
        commonApplicationNotificationService,
        accountService,
        logger)
    {
        _storeHelper = storeHelper;
        ShipState = shipState;

        FlyoutMenuItems.Add(new FlyoutMenuItem
        {
            Name = "Open",
            Value = "OPEN"
        });
    }

    // TODO: Replace with async next bump
    protected override void OnSelected(bool firstTime)
    {
        if (!ShipState.Value.Ships.Any() &&
            !ShipState.Value.Loading)
        {
            _ = LoadAllShipsAsync();
        }
    }

    private async Task LoadAllShipsAsync()
    {
        await _storeHelper.DispatchAndWaitForResponse<
            FetchShipsAction,
            UpdateShipsActionResponse>(
            new()
            {
                Identifier = AccountIdentifier
            });
    }


    [ObservableProperty]
    private ShipDto? _selectedItem;

    [RelayCommand]
    private void CommandMenu(string value)
    {
        if (SelectedItem is { } ship)
        {
            if (value == "OPEN")
            {
                OpenShip(ship);
            }
        }
    }

    private void OpenShip(ShipDto ship)
    {
        _applicationNotificationService.OpenShip(ship.Symbol);
    }

    public ObservableCollection<FlyoutMenuItem> FlyoutMenuItems { get; } = [];

    public override string Name => "Ships";
    public override string Id => PageConstants.ShipsPageId;
}
