using UnityEngine;
using UnityEngine.Events;

public class Shield : MonoBehaviour, IDamageable, IDestroyable
{
    [SerializeField] private float _shieldDamage = 100;
    [SerializeField] private float _shieldPower = 0;
    [SerializeField] private Animator _animator;
    [Space]
    [SerializeField] private UnityEvent OnDeploy;
    [SerializeField] private UnityEvent OnBreak;
    [SerializeField] private UnityEvent OnDestroyed;

    const string DEPLOY_KEY = "Deploy";
    const string DESTROY_KEY = "Destroy";

    private ShieldState _state = ShieldState.Disabled;

    public void ApplyDamage(float damage)
    {
        if (_state != ShieldState.Deployed) { return; }
        if (damage <= _shieldPower) { return; }
        _state = ShieldState.Destroying;
        _animator.SetTrigger(DESTROY_KEY);
        OnBreak.Invoke();
    }

    public void Deploy()
    {
        if (_state == ShieldState.Deployed) { return; }
        gameObject.SetActive(true);
        _state = ShieldState.Deployed;
        _animator.SetTrigger(DEPLOY_KEY);
        OnDeploy.Invoke();
    }

    public void Destroy()
    {
        if (_state != ShieldState.Destroying) { return; }
        _state = ShieldState.Disabled;
        OnDestroyed.Invoke();
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.ApplyDamage(_shieldDamage);
        }
    }

    private enum ShieldState { Disabled, Deployed, Destroying }
}
