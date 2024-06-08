namespace mark.davison.spacetraders.web.components.Pages;

public partial class Dashboard
{
    [Inject]
    public required IState<AccountState> AccountState { get; set; }

    [Inject]
    public required IDialogService DialogService { get; set; }

    [Inject]
    public required IAccountContextService AccountContextService { get; set; }

    private List<CommandMenuItem> _commandMenuItems { get; } =
        [
            new CommandMenuItem
            {
                Id = "ACTIVATE",
                Text = "Activate"
            }
        ];

    private async Task CommandMenuItemSelected(CommandMenuItem item, AccountDto account)
    {
        if (item.Id == "ACTIVATE")
        {
            await AccountContextService.ActivateAccountAsync(account, CancellationToken.None);
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
