using System.Collections;
using UnityEngine;
using DG.Tweening;

public class DefaultCannon : AbstractCannon
{
    public override bool IsShooting => _coroutine != null;

    [SerializeField] private Projectile _projectile;
    [SerializeField] private float _projectileDamage = 1;
    [SerializeField] private float _projectileVelocity = 10;
    [SerializeField] private float _shootDelay = 1;
    [SerializeField] private Transform _spawnPivot;
    [SerializeField] private SpriteRenderer _flash;
    [SerializeField] private float _flashTime;

    private Coroutine _coroutine = null;
    private YieldInstruction _delayInstruction;
    private bool _stopInvoked;

    public override void StartShoot()
    {
        _stopInvoked = false;
        if (_coroutine != null) { return; }
        _coroutine = StartCoroutine(ShootRoutine());
    }

    public override void StopShoot() => _stopInvoked = true;

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
        var projectile = Instantiate(_projectile, _spawnPivot.position, Quaternion.identity);
        var projectileVelocity = (Vector2)_spawnPivot.right * _projectileVelocity;
        _flash.DOKill();
        _flash.DOFade(0f, _flashTime).ChangeStartValue(_flash.color + Color.black).SetEase(Ease.OutCubic);
        projectile.Init(projectileVelocity, _projectileDamage);
    }

    private void Awake() => _delayInstruction = new WaitForSeconds(_shootDelay);

    private void OnDisable() => StopShoot();

    private void OnDestroy() => _flash.DOKill();
}
