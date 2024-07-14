using UnityEngine;

public class AsteroidEmitterTarget : EmitterTarget
{
    private Rigidbody2D _rigidbody;

    public override void SetAngularVelocity(float angularVelocity) => _rigidbody.angularVelocity = angularVelocity;
    public override void SetVelocity(Vector2 velocity) { _rigidbody.velocity = velocity; }
    public override void SetScale(float scale) => transform.localScale *= scale;

    private void Awake() => _rigidbody = GetComponent<Rigidbody2D>();
}
