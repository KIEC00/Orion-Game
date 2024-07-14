using System;
using System.Collections;
using UnityEngine;

public class DefaultCannon : AbstractCannon
{
    public override bool IsShooting => _coroutine != null;

    [SerializeField] private Projectile _projectile;
    [SerializeField] private float _projectileDamage = 1;
    [SerializeField] private float _projectileVelocity = 10;
    [SerializeField] private float _shootDelay = 1;
    [SerializeField] private Transform _pivot;

    private Rigidbody2D _rigidbody;

    private Coroutine _coroutine = null;
    private YieldInstruction _delayInstruction;
    private bool _stopInvoked;

    public override void StartShoot()
    {
        _stopInvoked = false;
        if (_coroutine != null) { return; }
        _coroutine = StartCoroutine(ShootRoutine());
    }

    public override void StopShoot()
    {
        _stopInvoked = true;
    }

    private IEnumerator ShootRoutine()
    {
        while (!_stopInvoked)
        {
            Shoot();
            yield return _delayInstruction;
        }
        _coroutine = null;
    }

    private void Shoot()
    {
        var launcherVelocity = _rigidbody.velocity;
        launcherVelocity.x = MathF.Max(-_projectileVelocity / 2, launcherVelocity.x);
        var projectile = Instantiate(_projectile, _pivot.position, Quaternion.identity);
        projectile.Init((Vector2)_pivot.right * _projectileVelocity + launcherVelocity, _projectileDamage);
    }

    private void Awake()
    {
        _delayInstruction = new WaitForSeconds(_shootDelay);
        _rigidbody = GetComponentInParent<Rigidbody2D>();
    }
}
