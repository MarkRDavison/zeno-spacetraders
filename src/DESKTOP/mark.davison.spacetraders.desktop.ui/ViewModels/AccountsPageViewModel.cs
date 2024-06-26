using Humanizer;

namespace mark.davison.spacetraders.desktop.ui.ViewModels;

public partial class AccountsPageViewModel : BasicApplicationPageViewModel
{
    private readonly IClientHttpRepository _clientHttpRepository;
    private readonly IApplicationNotificationService _applicationNotificationService;
    private readonly ICommonApplicationNotificationService _commonApplicationNotificationService;
    private readonly IAccountService _accountService;
    private readonly IAgentService _agentService;
    private readonly IDialogService _dialogService;
    private readonly IStoreHelper _storeHelper;

    public AccountsPageViewModel(
        IClientHttpRepository clientHttpRepository,
        IApplicationNotificationService applicationNotificationService,
        ICommonApplicationNotificationService commonApplicationNotificationService,
        IAccountService accountService,
        IAgentService agentService,
        IDialogService dialogService,
        IStoreHelper storeHelper)
    {
        _clientHttpRepository = clientHttpRepository;
        _applicationNotificationService = applicationNotificationService;
        _commonApplicationNotificationService = commonApplicationNotificationService;
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
        _storeHelper = storeHelper;
    }

    protected override async Task OnSelectedAsync(bool firstTime)
    {
        if (firstTime)
        {
            await FetchStatus();
            await FetchAccounts();
        }
    }

    [ObservableProperty]
    private AccountModel? _selectedItem;

    [RelayCommand]
    private void CommandMenu(string value)
    {
        var accountIdentifier = SelectedItem?.Identifier;
        if (!string.IsNullOrEmpty(accountIdentifier) && SelectedItem?.Id is not null)
        {
            if (value == "ACTIVATE")
            {
                _accountService.SetActiveAccount(SelectedItem!);
                _storeHelper.Dispatch(new ResetStateAction());
                _commonApplicationNotificationService.NotifyPageEnabledStateChanged();
                _commonApplicationNotificationService.ChangePage(PageGroupConstants.ContractGroupId, PageConstants.ContractsPageId);
                _ = _agentService.UpdateMyAgentAsync();
            }
            else if (value == "DELETE")
            {
                _ = DeleteAccountAsync(SelectedItem.Id);
            }
        }
    }

    private async Task DeleteAccountAsync(Guid id)
    {
        var response = await _clientHttpRepository.Post<DeleteAgentCommandResponse, DeleteAgentCommandRequest>(new()
        {
            AccountId = id
        }, CancellationToken.None);

        if (response.Success)
        {
            if (_accountService.HasActiveAccount && _accountService.GetActiveAccount().Id == id)
            {
                _storeHelper.Dispatch(new ResetStateAction());
                _accountService.UnselectActiveAccount();
            }
            Dispatcher.UIThread.Invoke(() =>
            {
                SelectedItem = null;
                Accounts.Remove(Accounts.First(_ => _.Id == id));
            });
        }
        else
        {
            // TODO: Snackbar
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

    public async Task FetchStatus(CancellationToken cancellationToken = default)
    {
        var response = await _clientHttpRepository.Get<FetchServerStatusQueryResponse, FetchServerStatusQueryRequest>(cancellationToken);

        _accountService.SetServerStatus(response.Value);
        OnPropertyChanged(nameof(ServerStatus));
        OnPropertyChanged(nameof(ServerNextReset));
        OnPropertyChanged(nameof(ServerResetDate));
    }
    public async Task FetchAccounts(CancellationToken cancellationToken = default)
    {
        var response = await _clientHttpRepository.Get<FetchAccountsQueryResponse, FetchAccountsQueryRequest>(cancellationToken);

        Dispatcher.UIThread.Invoke(() =>
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
    public override string Id => PageConstants.AccountsPageId;

    public ServerStatusDto? ServerStatus => _accountService.GetServerStatus();

    public string? ServerResetDate => ServerStatus?.Reset.Humanize();

    public string? ServerNextReset => ServerStatus?.NextReset.Humanize();
}
