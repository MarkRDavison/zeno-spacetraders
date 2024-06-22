namespace mark.davison.spacetraders.desktop.ui.Services;

public class PageEventArgs : EventArgs
{
    public PageEventArgs(Page page)
    {
        Page = page;
    }

    public Page Page { get; }
}

public class AccountChangeArgs : EventArgs
{

}

public class AgentChangeArgs : EventArgs
{

}

public interface IApplicationNotificationService
{
    event EventHandler<PageEventArgs> PageChanged;
    void ChangePage(Page page);

    event EventHandler<AccountChangeArgs> AccountChanged;
    void ChangeAccount();

    event EventHandler<AgentChangeArgs> AgentChanged;
    void ChangeAgent();
}
