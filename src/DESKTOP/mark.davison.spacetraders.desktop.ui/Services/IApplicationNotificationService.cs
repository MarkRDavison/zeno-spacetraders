namespace mark.davison.spacetraders.desktop.ui.Services;

public class AccountChangeArgs : EventArgs
{

}

public class AgentChangeArgs : EventArgs
{

}

public class RequestOpenShipArgs : EventArgs
{
    public RequestOpenShipArgs(string shipSymbol)
    {
        ShipSymbol = shipSymbol;
    }

    public string ShipSymbol { get; }
}

public interface IApplicationNotificationService
{

    event EventHandler<AccountChangeArgs> AccountChanged;
    void ChangeAccount();

    event EventHandler<AgentChangeArgs> AgentChanged;
    void ChangeAgent();

    event EventHandler<RequestOpenShipArgs> OpenShipRequested;
    void OpenShip(string shipSymbol);

}
