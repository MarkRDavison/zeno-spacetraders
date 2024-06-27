namespace mark.davison.spacetraders.desktop.ui.ViewModels;

public partial class ContractsPageViewModel : MainApplicationPageViewModel
{
    private readonly ISpacetradersStoreHelper _storeHelper;

    public IState<ContractState> ContractState { get; }

    public ContractsPageViewModel(
        IApplicationNotificationService applicationNotificationService,
        ICommonApplicationNotificationService commonApplicationNotificationService,
        IAccountService accountService,
        ISpacetradersStoreHelper storeHelper,
        IState<ContractState> contractState,
        ILogger<ContractsPageViewModel> logger
    ) : base(
        applicationNotificationService,
        commonApplicationNotificationService,
        accountService,
        logger)
    {
        _storeHelper = storeHelper;
        ContractState = contractState;

        FlyoutMenuItems.Add(new FlyoutMenuItem
        {
            Name = "Open",
            Value = "OPEN"
        });
    }

    protected override async Task OnSelectedAsync(bool firstTime)
    {
        await _storeHelper.EnsureContractsLoadedAsync(ContractState);
    }

    [RelayCommand]
    private void CommandMenu(string value)
    {
        if (SelectedItem is { } contract)
        {
            if (value == "OPEN")
            {
                OpenContract(contract);
            }
        }
    }

    private void OpenContract(ContractDto contract)
    {
        _applicationNotificationService.OpenContract(contract.Id);
    }

    [ObservableProperty]
    private ContractDto? _selectedItem;

    [ObservableProperty]
    private bool _loading = true;

    public override string Name => "Contracts";
    public override string Id => PageConstants.ContractsPageId;

    public ObservableCollection<FlyoutMenuItem> FlyoutMenuItems { get; } = [];
}
