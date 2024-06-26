namespace mark.davison.spacetraders.desktop.ui.ViewModels;

public partial class ContractsPageViewModel : MainApplicationPageViewModel
{
    private readonly ISpacetradersStoreHelper _storeHelper;

    public IState<ContractState> ContractState { get; }

    public ContractsPageViewModel(
        IApplicationNotificationService applicationNotificationService,
        IAccountService accountService,
        ISpacetradersStoreHelper storeHelper,
        IState<ContractState> contractState,
        ILogger<ContractsPageViewModel> logger
    ) : base(
        applicationNotificationService,
        accountService,
        logger)
    {
        _storeHelper = storeHelper;
        ContractState = contractState;
    }

    protected override async Task OnSelectedAsync(bool firstTime)
    {
        await _storeHelper.EnsureContractsLoadedAsync(ContractState);
    }

    [ObservableProperty]
    private ContractDto? _selectedItem;

    [ObservableProperty]
    private bool _loading = true;

    public override string Name => "Contracts";
    public override string Id => PageConstants.ContractsPageId;
}
