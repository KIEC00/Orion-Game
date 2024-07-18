using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlanetBackGroundSpawner : ParallaxBackgroundComponent, IGameBoundsChangeHandler
{
    [SerializeField] private Vector2 _rangeDistanceBetweenPlanets;
    [SerializeField] private Vector2 _scaleRange;
    [SerializeField] private int _renderOrder;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private PlanetRenderer _prefab;

    private float _distanceToNextPlanet;
    private readonly Queue<PlanetRenderer> _renderers = new();
    private Vector2 _size;

    protected override void Move(float delta)
    {
        _distanceToNextPlanet -= delta;
        if (_distanceToNextPlanet <= 0)
        {
            SpawnOrPoolPlanet();
            _distanceToNextPlanet = Random.Range(_rangeDistanceBetweenPlanets.x, _rangeDistanceBetweenPlanets.y);
        }
        foreach (var renderer in _renderers) { renderer.transform.Translate(-delta, 0, 0); }
    }

    private void SpawnOrPoolPlanet()
    {
        var renderer = (_renderers.Count == 0 || _renderers.Peek().IsVisible) ? Instantiate(_prefab, transform) : _renderers.Dequeue();
        ConfigureRenderer(renderer);
        _renderers.Enqueue(renderer);
    }

    private void ConfigureRenderer(PlanetRenderer renderer)
    {
        var sprite = _sprites[Random.Range(0, _sprites.Length)];
        var scale = Random.Range(_scaleRange.x, _scaleRange.y);
        renderer.Initialize(sprite, scale, _renderOrder);
        renderer.transform.localPosition = new(renderer.Bounds.extents.x + _size.x, Random.Range(-_size.y / 2, _size.y / 2));
    }

    public void OnGameBoundsChange(Rect bounds) => _size = bounds.size;

    private void Awake()
    {
        EventBus.AddListener<IGameBoundsChangeHandler>(this);
        _distanceToNextPlanet = Random.Range(0, _rangeDistanceBetweenPlanets.y);
    }

    private void OnDestroy() => EventBus.RemoveListener<IGameBoundsChangeHandler>(this);
}
