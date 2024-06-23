namespace mark.davison.spacetraders.desktop.common.Helpers;

public sealed class DisposableCallback : IDisposable
{
    private readonly string Id;
    private readonly Action Action;
    private bool Disposed;
    private readonly bool WasCreated;
    private readonly string CallerFilePath;
    private readonly int CallerLineNumber;

    public DisposableCallback(string id, Action action,
        [System.Runtime.CompilerServices.CallerFilePath] string callerFilePath = "",
        [System.Runtime.CompilerServices.CallerLineNumber] int callerLineNumber = 0
        )
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentNullException(nameof(id));
        }
        if (action is null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        Id = id;
        Action = action;
        WasCreated = true;
        CallerFilePath = callerFilePath;
        CallerLineNumber = callerLineNumber;
    }

    public void Dispose()
    {
        if (Disposed)
        {
            throw new ObjectDisposedException(
                nameof(DisposableCallback),
                $"Attempt to call {nameof(Dispose)} twice on {nameof(DisposableCallback)} with Id {GetIdInfo()}.");
        }

        Disposed = true;
        GC.SuppressFinalize(this);
        Action();
    }

    private string GetIdInfo()
    {
        string result = $"\"{Id}\"";
        result += $" (created in \"{CallerFilePath}\" on line {CallerLineNumber})";
        return result;
    }

    /// <summary>
    /// Throws an exception if this object is collected without being disposed
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the object is collected without being disposed</exception>
    ~DisposableCallback()
    {
        if (!Disposed && WasCreated)
        {
            string message = $"{nameof(DisposableCallback)} with Id \"{GetIdInfo()}\" was not disposed";
            throw new InvalidOperationException(
                $"{message}. See https://github.com/mrpmorris/Fluxor/tree/master/Docs/disposable-callback-not-disposed.md" +
                $" for more details");
        }
    }
}