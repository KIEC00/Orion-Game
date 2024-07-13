using System.Collections.Generic;

public class ListenerSet<TListener> : HashSet<TListener>, IEnumerable<TListener>
{
    private readonly List<(TListener listener, bool isAdd)> _buffer = new();
    private bool _unlock = true;

    public new void Add(TListener listener)
    {
        if (_unlock) { base.Add(listener); }
        else { _buffer.Add((listener, true)); }
    }

    public new void Remove(TListener listener)
    {
        if (_unlock) { base.Remove(listener); }
        else { _buffer.Add((listener, false)); }
    }

    public void Lock()
    {
        _unlock = false;
    }

    public void Unlock()
    {
        _unlock = true;
        if (_buffer.Count == 0) { return; }
        foreach (var (listener, isAdd) in _buffer)
        {
            if (isAdd) { base.Add(listener); }
            else { base.Remove(listener); }
        }
        _buffer.Clear();
    }
}
