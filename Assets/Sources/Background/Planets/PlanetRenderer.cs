using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlanetRenderer : MonoBehaviour
{
    public bool IsVisible => _renderer.isVisible;
    public Bounds Bounds => _renderer.bounds;

    private SpriteRenderer _renderer;

    public void Initialize(Sprite sprite, float scale, int renderOrder)
    {
        _renderer.sprite = sprite;
        _renderer.sortingOrder = renderOrder;
        transform.localScale = new(scale, scale, 1);
    }

    private void Awake() => _renderer = GetComponent<SpriteRenderer>();
}
