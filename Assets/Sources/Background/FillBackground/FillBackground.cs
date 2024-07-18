using System.Linq;
using UnityEngine;

public class FillBackground : ParallaxBackgroundComponent
{
    private (Transform anchor, float width)[] _renderers;
    private float _totalWidth = 0f;
    private int _current = 0;

    protected override void Move(float delta)
    {
        var current = _renderers[_current];
        foreach (var (anchor, _) in _renderers) { anchor.Translate(-delta, 0, 0); }
        if (current.anchor.localPosition.x < -current.width)
        {
            current.anchor.Translate(_totalWidth, 0, 0);
            _current = (_current + 1) % _renderers.Length;
        }
    }

    private void Awake()
    {
        _renderers = GetComponentsInChildren<SpriteRenderer>()
            .Select((renderer) => (renderer.transform, renderer.bounds.size.x)).ToArray();
        _totalWidth = 0;
        foreach (var (anchor, width) in _renderers)
        {
            var position = anchor.localPosition;
            position.x = _totalWidth;
            _totalWidth += width;
            anchor.localPosition = position;
        }
    }
}
