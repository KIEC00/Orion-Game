using UnityEngine;

public abstract class EmitterTarget : MonoBehaviour
{
    public abstract void SetVelocity(Vector2 velocity);
    public abstract void SetAngularVelocity(float angularVelocity);
    public abstract void SetScale(float scale);
}
