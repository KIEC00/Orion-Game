using UnityEngine;

public class ShieldPickUpEmitterTarget : EmitterTarget
{
    [SerializeField] private Rigidbody2D _rigidbody;

    public override void SetAngularVelocity(float angularVelocity) { }
    public override void SetScale(float scale) { }
    public override void SetVelocity(Vector2 velocity) { _rigidbody.velocity = new(velocity.x, 0); }
}
