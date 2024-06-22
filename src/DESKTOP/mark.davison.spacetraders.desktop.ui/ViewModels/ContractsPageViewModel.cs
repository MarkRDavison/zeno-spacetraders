namespace mark.davison.spacetraders.desktop.ui.ViewModels;

public partial class ContractsPageViewModel : MainApplicationPageViewModel
{
    private readonly IContractService _contractService;

    public ContractsPageViewModel(
        IApplicationNotificationService applicationNotificationService,
        IAccountService accountService,
        IContractService contractService,
        ILogger<ContractsPageViewModel> logger
    ) : base(
        applicationNotificationService,
        accountService,
        logger)
    {
        _contractService = contractService;
    }

    protected override async void OnSelected(bool firstTime)
    {
        if (firstTime)
        {
            await FetchContracts();
        }
    }

    public async Task FetchContracts(CancellationToken cancellationToken = default)
    {
        var contracts = await _contractService.FetchContracts(cancellationToken);
        Dispatcher.UIThread.Invoke(() =>
        {
            Contracts.Clear();
            foreach (var c in contracts)
            {
                Contracts.Add(c);
            }
            Loading = false;
        });
    }

    [ObservableProperty]
    private ContractDto? _selectedItem;

    [ObservableProperty]
    private bool _loading = true;
    public ObservableCollection<ContractDto> Contracts { get; set; } = [];

    public override string Name => "Contracts";
}
