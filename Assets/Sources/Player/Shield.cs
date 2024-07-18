using UnityEngine;
using UnityEngine.Events;

public class Shield : MonoBehaviour, IDamageable, IDestroyable
{
    [SerializeField] private float _shieldDamage = 100;
    [SerializeField] private float _shieldPower = 0;
    [SerializeField] private Animator _animator;
    [Space]
    [SerializeField] private UnityEvent OnDeploy;
    [SerializeField] private UnityEvent OnDamaged;
    [SerializeField] private UnityEvent OnBreak;

    const string DESTROY_KEY = "Destroy";

    public void ApplyDamage(float damage)
    {
        if (damage <= _shieldPower) { OnDamaged.Invoke(); return; }
        OnBreak.Invoke();
        _animator.SetTrigger(DESTROY_KEY);
    }

    private void OnEnable() => OnDeploy.Invoke();

    public void Destroy() => gameObject.SetActive(false);

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.ApplyDamage(_shieldDamage);
        }
    }
}
