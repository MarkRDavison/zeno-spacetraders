namespace mark.davison.spacetraders.web.components.Forms.AddAccount;

public sealed class AddAccountFormSubmission : IFormSubmission<AddAccountFormViewModel>
{
    private readonly IStoreHelper _storeHelper;
    private readonly IClientNavigationManager _clientNavigationManager;
    private readonly ISnackbar _snackbar;

    public AddAccountFormSubmission(
        IStoreHelper storeHelper,
        IClientNavigationManager clientNavigationManager,
        ISnackbar snackbar)
    {
        _storeHelper = storeHelper;
        _clientNavigationManager = clientNavigationManager;
        _snackbar = snackbar;
    }

    public async Task<Response> Primary(AddAccountFormViewModel formViewModel)
    {
        var symbol = string.Empty;

        if (formViewModel.FactionSymbol != null && formViewModel.FactionSymbol != Guid.Empty)
        {
            symbol = formViewModel.FactionSymbols.First(_ => _.Id == formViewModel.FactionSymbol).Name.ToUpper();
        }

        var action = new AddAccountAction
        {
            AddExisting = formViewModel.AddExisting,
            Email = formViewModel.Email,
            FactionSymbol = symbol,
            Identifier = formViewModel.Identifier,
            Token = formViewModel.Token
        };

        var response = await _storeHelper.DispatchAndWaitForResponse<AddAccountAction, AddAccountActionResponse>(action);

        if (response.SuccessWithValue)
        {
            _snackbar.Add($"Added account '{response.Value.Identifier}'", Severity.Success);
        }

        foreach (var error in response.Errors)
        {
            _snackbar.Add(error, Severity.Error);
        }

        return response;
    }
}
