using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerControls))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField][Range(0, 100)] private float _acceleration;
    [SerializeField][Range(0, 20)] private float _velocity;
    [SerializeField][Range(0, 90)] private float _maxRotationAngle;
    [SerializeField] private UnityEvent<Vector2> OnMoveUpdate;

    private PlayerControls _controls;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _controls = GetComponent<PlayerControls>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var targetVelocity = _velocity * _controls.Move;
        var velocity = Vector2.MoveTowards(_rigidbody.velocity, targetVelocity, _acceleration * Time.fixedDeltaTime);
        var rotation = _maxRotationAngle * _controls.Move.y;

        _rigidbody.rotation = rotation;
        _rigidbody.velocity = velocity;

        OnMoveUpdate.Invoke(_controls.Move);
    }
}
