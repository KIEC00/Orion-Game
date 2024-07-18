using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Asteroid : MonoBehaviour, IDestroyable
{
    public Bounds Bounds => _sprite.bounds;

    [SerializeField] protected float _damage;
    protected ParticleSystem _particle;
    protected SpriteRenderer _sprite;
    protected Rigidbody2D _rigidbody;

    public void Destroy()
    {
        _rigidbody.simulated = false;
        _sprite.enabled = false;
        _particle.Play();
        Destroy(gameObject, _particle.main.duration);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.TryGetComponent<IDamageable>(out var damageable)) { return; }
        damageable.ApplyDamage(_damage);
    }

    private void Awake()
    {
        _rigidbody = GetComponentInChildren<Rigidbody2D>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _particle = GetComponentInChildren<ParticleSystem>();
    }
}
