using System;
using System.Linq;

[Serializable]
public class EmitterStage
{
    public int Total => targets.Sum(target => target.count);
    public bool isInfinite = false;
    public Interval interval;
    public ObjectCount[] targets;

    [Serializable]
    public class ObjectCount
    {
        public EmitterTarget component;
        public int count;
    }

    [Serializable]
    public struct Interval
    {
        public float from;
        public float to;
    }
}
