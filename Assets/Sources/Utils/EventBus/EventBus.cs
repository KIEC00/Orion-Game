using System;
using System.Collections.Generic;

public static class EventBus
{
    private static readonly Dictionary<Type, ListenerSet<object>> _listeners = new();

    public static void AddListener<TListener>(TListener listener) where TListener : class
    {
        var type = typeof(TListener);
        if (!_listeners.TryGetValue(type, out var typeListeners))
        {
            // if (!type.IsInterface) { throw new ArgumentException($"{type.Name} is not interface."); }
            typeListeners = new ListenerSet<object>();
            _listeners.Add(type, typeListeners);
        }
        typeListeners.Add(listener);
    }

    public static void RemoveListener<TListener>(TListener listener)
    {
        var type = typeof(TListener);
        if (!_listeners.TryGetValue(type, out var typeListeners)) { return; }
        typeListeners.Remove(listener);
        if (typeListeners.Count == 0) { _listeners.Remove(type); }
    }

    public static void Invoke<TListener>(Action<TListener> action) where TListener : class
    {
        var type = typeof(TListener);
        if (!_listeners.TryGetValue(type, out var typeListeners)) { return; }
        typeListeners.Lock();
        foreach (var listener in typeListeners)
        {
            action.Invoke(listener as TListener);
        }
        typeListeners.Unlock();
    }
}
