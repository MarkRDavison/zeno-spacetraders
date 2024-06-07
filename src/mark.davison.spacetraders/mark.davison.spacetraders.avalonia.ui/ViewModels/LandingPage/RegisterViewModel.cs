namespace mark.davison.spacetraders.avalonia.ui.ViewModels.LandingPage;


public partial class RegisterViewModel : ViewModelBase
{
    private readonly IRegisterViewModelService _registerViewModelService;

    public RegisterViewModel() : this(
        DependencyInjectionExtensions.DesignTimeServiceProvider.GetRequiredService<IRegisterViewModelService>())
    {

    }

    public RegisterViewModel(IRegisterViewModelService registerViewModelService)
    {
        _registerViewModelService = registerViewModelService;
    }

    [RelayCommand(CanExecute = nameof(CanRegisterAsync))]
    private async Task RegisterAsync(CancellationToken cancellationToken)
    {
        try
        {
            var token = await _registerViewModelService.RegisterAsync(RegisterModel, cancellationToken);

            if (!string.IsNullOrEmpty(token))
            {

            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private bool CanRegisterAsync()
    {
        return !string.IsNullOrEmpty(RegisterModel.Callsign) && RegisterModel.Faction != null;
    }

    public RegisterModel RegisterModel { get; } = new() { Callsign = string.Empty, Faction = FactionSymbol.COSMIC };

    public ObservableCollection<ComboBoxListItem<FactionSymbol?>> Factions { get; } = [new("Cosmic", FactionSymbol.COSMIC), new("Void", FactionSymbol.VOID)];
}
