namespace mark.davison.spacetraders.avalonia.ui.Services;

public interface IRegisterViewModelService
{
    Task<string> RegisterAsync(RegisterModel model, CancellationToken cancellationToken);
}
