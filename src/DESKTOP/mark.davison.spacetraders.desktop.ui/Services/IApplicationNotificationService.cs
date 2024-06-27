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

public class RequestOpenContractArgs : EventArgs
{
    public RequestOpenContractArgs(string contractId)
    {
        ContractId = contractId;
    }

    public string ContractId { get; }
}

public interface IApplicationNotificationService
{

    event EventHandler<AccountChangeArgs> AccountChanged;
    void ChangeAccount();

    event EventHandler<AgentChangeArgs> AgentChanged;
    void ChangeAgent();

    event EventHandler<RequestOpenShipArgs> OpenShipRequested;
    void OpenShip(string shipSymbol);

    event EventHandler<RequestOpenContractArgs> OpenContractRequested;
    void OpenContract(string contractId);

}
