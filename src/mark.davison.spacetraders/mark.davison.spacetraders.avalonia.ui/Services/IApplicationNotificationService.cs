namespace mark.davison.spacetraders.avalonia.ui.Services;

public interface IApplicationNotificationService
{
    event EventHandler<PersistedAccount> UserChanged;
    void OnUserChanged(PersistedAccount user);

    event EventHandler<NavigationRequest> NavigationRequest;
    void OnNavigationRequest(NavigationRequest request);
}
