namespace mark.davison.spacetraders.desktop.ui.ViewModels;

public partial class ManageShipViewModel : MainApplicationPageViewModel, IDisposable
{
    private readonly IStoreHelper _storeHelper;

    public ManageShipViewModel(
        IApplicationNotificationService applicationNotificationService,
        IAccountService accountService,
        ILogger<ManageShipViewModel> logger,
        IStoreHelper storeHelper,
        IState<ShipState> shipState) : base(
        applicationNotificationService,
        accountService,
        logger)
    {
        ShipState = shipState;
        ShipState.PropertyChanged += ShipState_PropertyChanged;
        _storeHelper = storeHelper;
    }

    public IState<ShipState> ShipState { get; }
    public override string Name => ShipSymbol;
    public override bool Disabled => false;
    public string ShipSymbol { get; private set; } = string.Empty;

    public ShipDto Ship => ShipState.Value.Ships.First(_ => _.Symbol == ShipSymbol);
    public ShipNavDto ShipNav => ShipState.Value.ShipNavs.First(_ => _.ShipSymbol == ShipSymbol);

    public void SetShipSymbol(string shipSymbol)
    {
        ShipSymbol = shipSymbol;
    }

    private void ShipState_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        OnPropertyChanged(string.Empty);
    }

    #region Orbit / Dock Ship

    public bool ChangeShipStatusVisible => ShipNav.Status == "IN_ORBIT" || ShipNav.Status == "DOCKED";
    public string ChangeShipStatusText => ShipNav.Status == "IN_ORBIT" ? "Dock" : "Orbit";

    [RelayCommand(CanExecute = nameof(ChangeShipStatusVisible))]
    private async Task ChangeShipStatus()
    {
        if (ShipNav.Status == "IN_ORBIT")
        {
            await ModifyShipStateAsync(new ModifyShipStateAction
            {
                Identifier = AccountIdentifier,
                ShipSymbol = ShipSymbol,
                SetDocked = true
            });
        }
        else if (ShipNav.Status == "DOCKED")
        {
            await ModifyShipStateAsync(new ModifyShipStateAction
            {
                Identifier = AccountIdentifier,
                ShipSymbol = ShipSymbol,
                SetOrbiting = true
            });
        }
    }

    private async Task ModifyShipStateAsync(ModifyShipStateAction action)
    {
        await _storeHelper.DispatchAndWaitForResponse<ModifyShipStateAction, UpdateShipsActionResponse>(action);
    }

    #endregion

    public void Dispose()
    {
        base.Dispose();
        ShipState.PropertyChanged -= ShipState_PropertyChanged;
    }
}
