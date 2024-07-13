using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private Transform _engineFire;
    [SerializeField] private Vector2 _engineFireMinimumScale;

    public void UpdateMove(Vector2 normalizedVelocity)
    {
        var power = (normalizedVelocity.x + 1) / 2;
        _engineFire.localScale = Vector2.Lerp(_engineFireMinimumScale, Vector2.one, power);
    }
}
