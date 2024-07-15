using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemySpaceship : MonoBehaviour, IDestroyable
{
    [SerializeField] float _velocity;
    [SerializeField] float _angularVelocity;
    [SerializeField] float _collisionDamage;

    [SerializeField] Transform _engineVisual;
    [SerializeField] AbstractCannon _canon;


    private Rigidbody2D _rigidbody;
    private Transform _player;

    private void FixedUpdate()
    {
        var direction = _player ? (Vector2)(_player.position - transform.position).normalized : Vector2.left;
        var targetAngle = Vector2.SignedAngle(Vector2.left, direction);
        var angle = Mathf.MoveTowardsAngle(_rigidbody.rotation, targetAngle, _angularVelocity * Time.deltaTime);
        _rigidbody.rotation = angle;
        _rigidbody.velocity = (Vector2)transform.right * -_velocity;
    }

    private IEnumerator Start()
    {
        var controls = FindObjectOfType<PlayerControls>();
        if (controls != null) { _player = controls.transform; }
        yield return new WaitForSeconds(.5f);
        _canon.StartShoot();
    }

    public void Explode()
    {
        _rigidbody.simulated = false;
        GetComponent<Animator>().enabled = true;
        Destroy(_engineVisual.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.TryGetComponent<IDamageable>(out var damageable)) { return; }
        damageable.ApplyDamage(_collisionDamage);
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Destroy() { Destroy(gameObject); }
}
