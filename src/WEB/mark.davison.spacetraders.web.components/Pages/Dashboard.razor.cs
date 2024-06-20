namespace mark.davison.spacetraders.web.components.Pages;

public partial class Dashboard
{
    [Inject]
    public required IState<AccountState> AccountState { get; set; }

    [Inject]
    public required IDialogService DialogService { get; set; }

    [Inject]
    public required IAccountContextService AccountContextService { get; set; }

    [Inject]
    public required IStoreHelper StoreHelper { get; set; }

    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    protected override void OnParametersSet()
    {
        var url = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);

        if (string.IsNullOrEmpty(url) || url == "/")
        {
            NavigationManager.NavigateTo(Routes.Accounts);
        }
    }

    private List<CommandMenuItem> _commandMenuItems { get; } =
        [
            new CommandMenuItem
            {
                Id = "ACTIVATE",
                Text = "Activate"
            },
            new CommandMenuItem
            {
                Id = "DELETE",
                Text = "Delete"
            }
        ];

    private async Task CommandMenuItemSelected(CommandMenuItem item, AccountDto account)
    {
        if (item.Id == "ACTIVATE")
        {
            await AccountContextService.ActivateAccountAsync(account, CancellationToken.None);
        }
        else if (item.Id == "DELETE")
        {
            await StoreHelper.DispatchAndWaitForResponse<DeleteAccountAction, DeleteAccountActionResponse>(new DeleteAccountAction
            {
                AccountId = account.Id
            });

            var activeAccount = AccountContextService.GetActiveAccount();

            if (account.Id == activeAccount?.Id)
            {
                AccountContextService.DeactivateAccount();
            }
        }
    }

    private async Task OpenAddAccountDialog()
    {
        var options = new DialogOptions
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Small,
            FullWidth = false
        };

        var param = new DialogParameters<FormModal<ModalViewModel<AddAccountFormViewModel, AddAccountForm>, AddAccountFormViewModel, AddAccountForm>>
        {
            { _ => _.PrimaryText, "Save" },
            { _ => _.Instance, null }
        };

        var dialog = DialogService.Show<FormModal<ModalViewModel<AddAccountFormViewModel, AddAccountForm>, AddAccountFormViewModel, AddAccountForm>>("Add account", param, options);

        await dialog.Result;
    }
}
