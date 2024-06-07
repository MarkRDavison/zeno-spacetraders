namespace mark.davison.spacetraders.avalonia.ui.Services;

public sealed class RegisterViewModelService : IRegisterViewModelService
{
    private readonly ISpaceTradersApiClient _apiClient;
    private readonly IAuthenticationContext _authenticationContext;

    public RegisterViewModelService(
        ISpaceTradersApiClient apiClient,
        IAuthenticationContext authenticationContext)
    {
        _apiClient = apiClient;
        _authenticationContext = authenticationContext;
    }

    public async Task<string> RegisterAsync(RegisterModel model, CancellationToken cancellationToken)
    {
        var response = await _apiClient.RegisterAsync(new RegisterBody
        {
            Faction = model.Faction!.Value,
            Symbol = model.Callsign
        }, cancellationToken);

        if (!string.IsNullOrEmpty(response.Data.Token))
        {
            _authenticationContext.Token = response.Data.Token;
        }

        return response.Data.Token;
    }
}
