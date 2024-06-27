namespace mark.davison.spacetraders.desktop.ui.ViewModels;

public partial class ManageShipViewModel : MainApplicationPageViewModel, IDisposable
{
    private readonly ISpacetradersStoreHelper _storeHelper;

    public ManageShipViewModel(
        IApplicationNotificationService applicationNotificationService,
        ICommonApplicationNotificationService commonApplicationNotificationService,
        IAccountService accountService,
        ILogger<ManageShipViewModel> logger,
        ISpacetradersStoreHelper storeHelper,
        IState<ShipState> shipState,
        IState<ContractState> contractState
    ) : base(
        applicationNotificationService,
        commonApplicationNotificationService,
        accountService,
        logger)
    {
        _storeHelper = storeHelper;
        ShipState = shipState;
        ContractState = contractState;

        Setup();
    }

    public IState<ShipState> ShipState { get; }
    public IState<ContractState> ContractState { get; }
    public ObservableCollection<FlyoutMenuItem> MainCommandMenuItems { get; } = [];

    public override string Name => ShipSymbol;
    public override bool IsClosable => true;
    public override bool Disabled => false;
    public string ShipSymbol { get; private set; } = string.Empty;

    public ShipDto Ship => ShipState.Value.Ships.First(_ => _.Symbol == ShipSymbol);
    public ShipNavDto ShipNav => ShipState.Value.ShipNavs.First(_ => _.ShipSymbol == ShipSymbol);

    protected override async Task OnSelectedAsync(bool firstTime)
    {
        await _storeHelper.EnsureContractsLoadedAsync(ContractState);

        if (firstTime && ContractState.Value.Loaded && !ContractState.Value.Contracts.Any())
        {
            MainCommandMenuItems.Add(new FlyoutMenuItem
            {
                Name = "Negotiate contract",
                Value = "NEGOTIATE"
            });
        }
    }

    public void SetShipSymbol(string shipSymbol)
    {
        ShipSymbol = shipSymbol;
    }

    private void ShipState_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        OnPropertyChanged(string.Empty);
    }

    [RelayCommand]
    private async Task MainCommandMenu(string value)
    {
        if (value == "NEGOTIATE")
        {
            if (ShipNav.Status != "DOCKED")
            {
                // TODO: DIALOG
                return;
            }
            if (await _storeHelper.DispatchAndWaitForResponse<NegotiateContractAction, UpdateContractsActionResponse>(new NegotiateContractAction
            {
                Identifier = AccountIdentifier,
                ShipSymbol = ShipSymbol
            }) is { SuccessWithValue: true })
            {
                MainCommandMenuItems.Remove(MainCommandMenuItems.First(_ => _.Value == value));
            }
        }
    }

    #region Orbit / Dock Ship

    public bool ChangeShipStatusVisible =>
        ShipNav.Status == "IN_ORBIT" ||
        ShipNav.Status == "DOCKED";

    public string ChangeShipStatusText =>
        ShipNav.Status == "IN_ORBIT"
            ? "Dock"
            : "Orbit";

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

    private void Setup()
    {
        ShipState.PropertyChanged += ShipState_PropertyChanged;
    }

    private void Teardown()
    {
        ShipState.PropertyChanged -= ShipState_PropertyChanged;
    }

    public void Dispose()
    {
        base.Dispose();
        Teardown();
    }
}
