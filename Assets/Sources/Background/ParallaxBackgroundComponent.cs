using UnityEngine;

public abstract class ParallaxBackgroundComponent : MonoBehaviour
{
    [SerializeField][Range(0, 2)] private float _parallaxScale = 1f;

    public void MoveScale(float delta) { Move(delta * _parallaxScale); }
    protected abstract void Move(float delta);
}
