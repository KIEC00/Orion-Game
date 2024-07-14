using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public Vector2 Move { get; set; }

    [SerializeField][Range(0, 100)] private float _acceleration;
    [SerializeField][Range(0, 20)] private float _velocity;
    [SerializeField][Range(0, 90)] private float _maxRotationAngle;
    [SerializeField] private UnityEvent<Vector2> OnMoveUpdate;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // TODO Edge collision detection
        var targetVelocity = _velocity * Move;
        var velocity = Vector2.MoveTowards(_rigidbody.velocity, targetVelocity, _acceleration * Time.fixedDeltaTime);
        var rotation = _maxRotationAngle * Move.y;

        _rigidbody.rotation = rotation;
        _rigidbody.velocity = velocity;

        OnMoveUpdate.Invoke(Move);
    }
}
