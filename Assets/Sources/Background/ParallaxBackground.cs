using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float Velocity { get => _moveVelocity; set => _moveVelocity = value; }

    [SerializeField] private float _moveVelocity;

    private ParallaxBackgroundComponent[] _parallaxes;

    private void Update()
    {
        foreach (var parallax in _parallaxes)
        {
            parallax.MoveScale(_moveVelocity * Time.deltaTime);
        }
    }

    private void Awake()
    {
        _parallaxes = GetComponentsInChildren<ParallaxBackgroundComponent>();
    }
}
