namespace mark.davison.spacetraders.desktop.ui.State;

public interface IDesktopActionSubscriber
{
    public void Notify(object action);
    void SubscribeToAction<TAction>(object subscriber, Action<TAction> callback);
    IDisposable GetActionUnsubscriberAsIDisposable(object subscriber);
}
