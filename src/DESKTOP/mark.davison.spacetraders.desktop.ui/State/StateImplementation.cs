namespace mark.davison.spacetraders.desktop.ui.State;

public sealed class StateImplementation<TState> : ObservableObject, IState<TState>
    where TState : IDesktopState, new()
{
    public StateImplementation()
    {
        StateStore.RegisterStateChangeCallback<TState>(Notify);
    }

    public TState Value => StateStore.GetState<TState>();

    public void Notify()
    {
        OnPropertyChanged(nameof(Value));
    }
}
