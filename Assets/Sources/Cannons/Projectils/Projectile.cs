using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    const string EXPLODE_KEY = "Explode";

    [SerializeField] private UnityEvent OnShot;
    [SerializeField] private UnityEvent OnExplode;
    [Space]
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Animator _animator;

    private float _damage;

    public void Init(Vector2 velocity, float damage)
    {
        _rigidbody.velocity = velocity;
        _rigidbody.rotation = Vector2.SignedAngle(Vector2.right, velocity);
        _damage = damage;
        OnShot.Invoke();
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<IDamageable>(out var damageable)) { damageable.ApplyDamage(_damage); };
        _rigidbody.simulated = false;
        _animator.SetTrigger(EXPLODE_KEY);
        OnExplode.Invoke();
    }

    private void OnValidate()
    {
        if (_rigidbody == null) { _rigidbody = GetComponent<Rigidbody2D>(); }
        if (_animator == null) { _animator = GetComponent<Animator>(); }
    }
}
