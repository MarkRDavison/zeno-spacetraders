namespace mark.davison.spacetraders.desktop.ui.ViewModels;

public partial class AccountsPageViewModel : BasicApplicationPageViewModel
{
    private readonly IClientHttpRepository _clientHttpRepository;
    private readonly IApplicationNotificationService _applicationNotificationService;
    private readonly IAccountService _accountService;
    private readonly IAgentService _agentService;
    private readonly IDialogService _dialogService;

    public AccountsPageViewModel(
        IClientHttpRepository clientHttpRepository,
        IApplicationNotificationService applicationNotificationService,
        IAccountService accountService,
        IAgentService agentService,
        IDialogService dialogService)
    {
        _clientHttpRepository = clientHttpRepository;
        _applicationNotificationService = applicationNotificationService;
        _accountService = accountService;
        _agentService = agentService;
        _dialogService = dialogService;

        FlyoutMenuItems.Add(new FlyoutMenuItem
        {
            Name = "Activate",
            Value = "ACTIVATE"
        });
        FlyoutMenuItems.Add(new FlyoutMenuItem
        {
            Name = "Delete",
            Value = "DELETE"
        });
    }

    protected override async void OnSelected(bool firstTime)
    {
        if (firstTime)
        {
            await FetchAccounts();
        }
    }

    [ObservableProperty]
    private AccountModel? _selectedItem;

    [RelayCommand]
    private void CommandMenu(string value)
    {
        var accountIdentifier = SelectedItem?.Identifier;
        if (!string.IsNullOrEmpty(accountIdentifier))
        {
            if (value == "ACTIVATE")
            {
                _accountService.SetActiveAccount(SelectedItem!);
                _applicationNotificationService.ChangePage(Page.Contracts);
                _ = _agentService.UpdateMyAgentAsync();
            }
        }
    }

    [RelayCommand]
    private async Task RegisterNewAgent(CancellationToken cancellationToken)
    {
        var response = await _dialogService.ShowDialogAsync<Response<AgentDto>, RegisterAgentDialogViewModel>(
            new(),
            new DialogSettings
            {
                Title = "Register new agent",
                PrimaryText = "Register"
            });

        if (response is not null && response.Success)
        {
            await FetchAccounts(cancellationToken);
        }
    }

    public async Task FetchAccounts(CancellationToken cancellationToken = default)
    {
        var response = await _clientHttpRepository.Get<FetchAccountsQueryResponse, FetchAccountsQueryRequest>(cancellationToken);

        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            if (response.SuccessWithValue)
            {
                Accounts.Clear();
                foreach (var a in response.Value)
                {
                    Accounts.Add(new AccountModel
                    {
                        Id = a.Id,
                        Identifier = a.Identifier,
                        Version = a.Version,
                        Email = a.Email
                    });
                }
            }
            else
            {
                // TODO: Snackbar dispatch
            }
            Loading = false;
        });
    }

    [ObservableProperty]
    private bool _loading = true;
    public ObservableCollection<AccountModel> Accounts { get; set; } = [];
    public ObservableCollection<FlyoutMenuItem> FlyoutMenuItems { get; } = [];

    public override string Name => "Accounts";
    public override bool Disabled => false;
}
