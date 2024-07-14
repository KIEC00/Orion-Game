using UnityEngine;

public class CannonAdapter : MonoBehaviour
{
    private AbstractCannon _cannon;

    public void StartShoot() { _cannon.StartShoot(); }
    public void StopShoot() { _cannon.StopShoot(); }

    public void SwitchCannon(AbstractCannon cannon)
    {
        var isShooting = cannon.IsShooting;
        if (isShooting) { _cannon.StopShoot(); }
        Destroy(_cannon);
        Instantiate(_cannon, transform);
        if (isShooting) { _cannon.StartShoot(); }
    }

    private void Awake()
    {
        _cannon = GetComponentInChildren<AbstractCannon>();
    }
}
