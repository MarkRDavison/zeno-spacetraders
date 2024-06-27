using Humanizer;

namespace mark.davison.spacetraders.desktop.ui.ViewModels;

public partial class ManageContractViewModel : MainApplicationPageViewModel
{
    private readonly ISpacetradersStoreHelper _storeHelper;

    public ManageContractViewModel(
        IApplicationNotificationService applicationNotificationService,
        ICommonApplicationNotificationService commonApplicationNotificationService,
        IAccountService accountService,
        ILogger<ManageContractViewModel> logger,
        ISpacetradersStoreHelper storeHelper,
        IState<ContractState> contractState
    ) : base(
        applicationNotificationService,
        commonApplicationNotificationService,
        accountService,
        logger)
    {
        _storeHelper = storeHelper;
        ContractState = contractState;

        RegisterStateChange(ContractState);
    }

    public IState<ContractState> ContractState { get; }

    public string ContractId { get; private set; } = string.Empty;
    public override bool IsClosable => true;
    public override string Name => $"{Contract.Type} contract";
    public override string Id => ContractId;
    public ContractDto Contract => ContractState.Value.Contracts.First(_ => _.Id == ContractId);

    #region UI properties

    public string ExpireText => $"This contract will expire {(Contract.Accepted ? Contract.Terms.Deadline : Contract.DeadlineToAccept).Humanize()}";

    #endregion

    [RelayCommand]
    private async Task Accept()
    {
        await _storeHelper.DispatchAndWaitForResponse<AcceptContractAction, UpdateContractsActionResponse>(new AcceptContractAction
        {
            Identifier = AccountIdentifier,
            ContractId = ContractId
        });
    }

    protected override async Task OnSelectedAsync(bool firstTime)
    {
        await _storeHelper.EnsureContractsLoadedAsync(ContractState);
    }

    public void SetContractId(string contractId)
    {
        ContractId = contractId;
    }

    protected override void StatePropertyChanged(string name)
    {
        if (name == nameof(Store.ContractUseCase.ContractState))
        {
            OnPropertyChanged(string.Empty);
        }
    }
}
