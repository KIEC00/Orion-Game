using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GameBounds : MonoBehaviour, IGameBoundsChangeHandler
{
    [SerializeField] private float _offset;

    private BoxCollider2D _collider;
    private Rect _bounds;

    public void OnGameBoundsChange(Rect bounds)
    {
        bounds.size += Vector2.one* _offset;
        _bounds = bounds;
        
        _collider.enabled = false;
        _collider.offset = bounds.center;
        _collider.size = bounds.size;
        _collider.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_bounds.Contains(other.transform.position)) { return; }
        Destroy(other.gameObject);
    }

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        EventBus.AddListener<IGameBoundsChangeHandler>(this);
    }

    private void OnDestroy()
    {
        EventBus.RemoveListener<IGameBoundsChangeHandler>(this);
    }
}
