namespace mark.davison.spacetraders.desktop.ui.Services;

public class AccountChangeArgs : EventArgs
{

}

public class AgentChangeArgs : EventArgs
{

}

public interface IApplicationNotificationService
{

    event EventHandler<AccountChangeArgs> AccountChanged;
    void ChangeAccount();

    event EventHandler<AgentChangeArgs> AgentChanged;
    void ChangeAgent();
}
