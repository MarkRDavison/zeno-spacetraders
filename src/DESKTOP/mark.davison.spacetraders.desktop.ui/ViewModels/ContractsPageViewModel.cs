using mark.davison.spacetraders.desktop.ui.Constants;
using mark.davison.spacetraders.desktop.ui.Store.ContractUseCase;

namespace mark.davison.spacetraders.desktop.ui.ViewModels;

public partial class ContractsPageViewModel : MainApplicationPageViewModel
{
    private readonly IStoreHelper _storeHelper;

    public IState<ContractState> ContractState { get; }

    public ContractsPageViewModel(
        IApplicationNotificationService applicationNotificationService,
        IAccountService accountService,
        IStoreHelper storeHelper,
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

    protected override void OnSelected(bool firstTime)
    {
        if (!ContractState.Value.Contracts.Any() &&
            !ContractState.Value.Loading)
        {
            _ = FetchContracts();
        }
    }

    public async Task FetchContracts(CancellationToken cancellationToken = default)
    {
        await _storeHelper.DispatchAndWaitForResponse<FetchContractsAction, UpdateContractsActionResponse>(new FetchContractsAction
        {
            Identifier = AccountIdentifier
        });
    }

    [ObservableProperty]
    private ContractDto? _selectedItem;

    [ObservableProperty]
    private bool _loading = true;

    public override string Name => "Contracts";
    public override string Id => PageConstants.ContractsPageId;
}
