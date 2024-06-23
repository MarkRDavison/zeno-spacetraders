using mark.davison.spacetraders.desktop.common.Helpers;
using System.Linq;

namespace mark.davison.spacetraders.desktop.ui.State;

public sealed class DesktopActionSubscriber : IDesktopActionSubscriber
{
    private readonly object SyncRoot = new();
    private readonly Dictionary<object, List<ActionSubscription>> SubscriptionsForInstance = new();
    private readonly Dictionary<Type, List<ActionSubscription>> SubscriptionsForType = new();

    public IDisposable GetActionUnsubscriberAsIDisposable(object subscriber) =>
        new DisposableCallback(
            id: $"{nameof(DesktopActionSubscriber)}.{nameof(GetActionUnsubscriberAsIDisposable)}",
            action: () => UnsubscribeFromAllActions(subscriber));

    public void Notify(object action)
    {
        if (action is null)
            throw new ArgumentNullException(nameof(action));

        lock (SyncRoot)
        {
            IEnumerable<Action<object>> callbacks =
                SubscriptionsForType
                    .Where(x => x.Key.IsAssignableFrom(action.GetType()))
                    .SelectMany(x => x.Value)
                    .Select(x => x.Callback)
                    .ToArray();
            foreach (Action<object> callback in callbacks)
            {
                callback(action);
            }
        }
    }

    public void SubscribeToAction<TAction>(object subscriber, Action<TAction> callback)
    {
        if (subscriber is null)
        {
            throw new ArgumentNullException(nameof(subscriber));
        }
        if (callback is null)
        {
            throw new ArgumentNullException(nameof(callback));
        }

        var subscription = new ActionSubscription(
            subscriber: subscriber,
            actionType: typeof(TAction),
            callback: (object action) => callback((TAction)action));

        lock (SyncRoot)
        {
            if (!SubscriptionsForInstance.TryGetValue(subscriber, out var instanceSubscriptions))
            {
                instanceSubscriptions = new List<ActionSubscription>();
                SubscriptionsForInstance[subscriber] = instanceSubscriptions;
            }
            instanceSubscriptions.Add(subscription);

            if (!SubscriptionsForType.TryGetValue(typeof(TAction), out var typeSubscriptions))
            {
                typeSubscriptions = new List<ActionSubscription>();
                SubscriptionsForType[typeof(TAction)] = typeSubscriptions;
            }
            typeSubscriptions.Add(subscription);
        };
    }


    public void UnsubscribeFromAllActions(object subscriber)
    {
        if (subscriber is null)
        {
            throw new ArgumentNullException(nameof(subscriber));
        }

        lock (SyncRoot)
        {
            if (!SubscriptionsForInstance.TryGetValue(subscriber, out var instanceSubscriptions))
            {
                return;
            }

            IEnumerable<object> subscribedInstances =
                instanceSubscriptions
                    .Select(x => x.Subscriber)
                    .Distinct();

            IEnumerable<Type> subscribedActionTypes =
                instanceSubscriptions
                    .Select(x => x.ActionType)
                    .Distinct();

            foreach (Type actionType in subscribedActionTypes)
            {
                if (!SubscriptionsForType.TryGetValue(actionType, out var actionTypeSubscriptions))
                {
                    continue;
                }
                SubscriptionsForType[actionType] = actionTypeSubscriptions
                    .Except(instanceSubscriptions)
                    .ToList();
            }

            foreach (object subscription in subscribedInstances)
            {
                SubscriptionsForInstance.Remove(subscription);
            }
        }
    }
}
