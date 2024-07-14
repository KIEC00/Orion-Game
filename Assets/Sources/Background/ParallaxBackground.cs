using UnityEngine;

public class ParallaxBackground : MonoBehaviour, IGameFakeVelocityChangeHandler, IGameBoundsChangeHandler
{
    private float _velocity;
    private ParallaxBackgroundComponent[] _parallaxes;

    private void Update()
    {
        foreach (var parallax in _parallaxes)
        {
            parallax.MoveScale(_velocity * Time.deltaTime);
        }
    }

    public void OnFakeVelocityChange(float velocity) { _velocity = velocity; }

    public void OnGameBoundsChange(Rect bounds)
    {
        var position = transform.position;
        position.x = bounds.x;
        transform.position = position;
    }

    private void Awake()
    {
        _parallaxes = GetComponentsInChildren<ParallaxBackgroundComponent>();
        EventBus.AddListener<IGameFakeVelocityChangeHandler>(this);
        EventBus.AddListener<IGameBoundsChangeHandler>(this);
    }

    private void OnDestroy()
    {
        EventBus.RemoveListener<IGameFakeVelocityChangeHandler>(this);
        EventBus.RemoveListener<IGameBoundsChangeHandler>(this);
    }
}
