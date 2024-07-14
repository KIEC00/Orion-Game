using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerMovement : MonoBehaviour, IGameBoundsChangeHandler
{
    public Vector2 Move { get; set; }

    [SerializeField][Range(0, 100)] private float _acceleration;
    [SerializeField][Range(0, 20)] private float _velocity;
    [SerializeField][Range(0, 90)] private float _maxRotationAngle;
    [SerializeField] private UnityEvent<Vector2> OnMoveUpdate;
    [Space]
    [SerializeField] private Rigidbody2D _rigidbody;

    private Rect _bounds;
    private Rect _shipBounds;

    private void FixedUpdate()
    {
        var move = Move;
        var outOfBounds = UpdateCollisions();

        if (outOfBounds.x != 0f && MathF.Sign(move.x) == -MathF.Sign(outOfBounds.x)) { move.x = 0f; }
        if (outOfBounds.y != 0f && MathF.Sign(move.y) == -MathF.Sign(outOfBounds.y)) { move.y = 0f; }

        var targetVelocity = _velocity * move;
        var velocity = Vector2.MoveTowards(_rigidbody.velocity, targetVelocity, _acceleration * Time.fixedDeltaTime);
        var rotation = _maxRotationAngle * move.y;

        _rigidbody.rotation = rotation;
        _rigidbody.velocity = velocity;

        OnMoveUpdate.Invoke(move);
    }

    private Vector2 UpdateCollisions()
    {
        var moveBy = Vector2.zero;
        _shipBounds.center = _rigidbody.position;
        if (_shipBounds.xMin < _bounds.xMin) { moveBy.x = _bounds.xMin - _shipBounds.xMin; }
        else if (_shipBounds.xMax > _bounds.xMax) { moveBy.x = _bounds.xMax - _shipBounds.xMax; }
        if (_shipBounds.yMin < _bounds.yMin) { moveBy.y = _bounds.yMin - _shipBounds.yMin; }
        else if (_shipBounds.yMax > _bounds.yMax) { moveBy.y = _bounds.yMax - _shipBounds.yMax; }
        return moveBy;
    }

    public void OnGameBoundsChange(Rect bounds)
    {
        _bounds = bounds;
    }

    private void Awake()
    {
        _shipBounds.size = GetComponentInChildren<SpriteRenderer>().bounds.size;
        EventBus.AddListener<IGameBoundsChangeHandler>(this);
    }

    private void OnDestroy()
    {
        EventBus.RemoveListener<IGameBoundsChangeHandler>(this);
    }
}
