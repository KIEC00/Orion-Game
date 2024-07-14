using UnityEngine;

public abstract class AbstractCannon : MonoBehaviour
{
    public abstract bool IsShooting { get; }

    public abstract void StartShoot();
    public abstract void StopShoot();
}
