namespace mark.davison.spacetraders.desktop.ui.State;

public abstract class StateImplementation : ObservableObject
{
    public abstract void SetState(object state);
    public abstract object StateValue { get; }
}

public sealed class StateImplementation<TState> : StateImplementation, IState<TState>
    where TState : IDesktopState, new()
{
    public StateImplementation()
    {
        StateStore.RegisterStateChangeCallback<TState>(Notify);
    }

    public TState Value => StateStore.GetState<TState>();
    public override object StateValue => Value;

    public void Notify()
    {
        OnPropertyChanged(nameof(Value));
    }

    public override void SetState(object state)
    {
        if (state is TState typed)
        {
            StateStore.SetState<TState>(typed);
        }
    }
}
