using UnityEngine;

public abstract class ParallaxBackgroundComponent : MonoBehaviour
{
    [SerializeField][Range(0, 2)] float parallaxScale = 1f;

    public void MoveScale(float delta) { Move(delta * parallaxScale); }
    protected abstract void Move(float delta);
}
