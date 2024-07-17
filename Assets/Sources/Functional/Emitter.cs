using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Emitter : MonoBehaviour, IGameBoundsChangeHandler, IGameFakeVelocityChangeHandler
{
    [SerializeField] private EmitterStage[] _stages;
    [Space]
    [SerializeField] private Vector2 _velocityVariation;
    [SerializeField] private float _angularVelocityVariation;
    [SerializeField] private float _scaleVariation;
    [Space]
    [SerializeField] private float _minimalDistanceNormalized = 0.1f;
    [SerializeField] private float _spawnOffset;
    [Space]
    [SerializeField] private int _addPointInterval;

    private Vector2 _anchor;
    private float _amplitude;
    private float _velocity;
    private float _previousPosition = float.MaxValue;

    private IEnumerator SpawnRoutine()
    {
        foreach (var stage in _stages)
        {
            var interval = stage.interval;
            while (true)
            {
                var target = SelectTarget(stage);
                if (target == null) { break; }
                Spawn(target);
                yield return new WaitForSeconds(Random.Range(interval.from, interval.to));
            }
        }
    }

    private IEnumerator AddPointRoutine(YieldInstruction instruction)
    {
        while (true)
        {
            yield return instruction;
            EventBus.Invoke<IScoreAddHandler>(obj => obj.OnScoreAdd(1));
        }
    }

    private void Spawn(EmitterTarget target)
    {
        var velocity = new Vector2(Random.Range(-_velocityVariation.x, _velocityVariation.x) - _velocity, Random.Range(-_velocityVariation.y, _velocityVariation.y));
        var angularVelocity = Random.Range(-_angularVelocityVariation, _angularVelocityVariation);
        var scale = Random.Range(-_scaleVariation, _scaleVariation) + 1f;
        var position = GetRandomPosition();

        var targetObject = Instantiate(target, position, Quaternion.identity);
        targetObject.SetVelocity(velocity);
        targetObject.SetAngularVelocity(angularVelocity);
        targetObject.SetScale(scale);
    }

    private Vector2 GetRandomPosition()
    {
        var position = Vector2.zero;
        for (var i = 0; i < 10; i++)
        {
            position.y = Random.Range(-.5f, .5f);
            if (MathF.Abs(position.y - _previousPosition) >= _minimalDistanceNormalized) { break; }
        };
        _previousPosition = position.y;
        position.y *= _amplitude;
        return position + _anchor;
    }

    private EmitterTarget SelectTarget(EmitterStage stage)
    {
        var total = stage.Total;
        if (total == 0) { return null; }

        var target = Random.Range(1, total + 1);
        var current = 0;
        foreach (var objectCount in stage.targets)
        {
            current += objectCount.count;
            if (current < target) { continue; }
            if (!stage.isInfinite) { objectCount.count--; };
            return objectCount.component;
        }

        throw new Exception("Something went wrong at selecting target");
    }

    public void Run()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(AddPointRoutine(new WaitForSeconds(_addPointInterval)));
    }

    public void OnFakeVelocityChange(float velocity) => _velocity = velocity;
    public void OnGameBoundsChange(Rect bounds)
    {
        _anchor = new Vector2(bounds.xMax + _spawnOffset, bounds.center.y);
        _amplitude = bounds.height / 2;
    }

    private void Awake()
    {
        EventBus.AddListener<IGameBoundsChangeHandler>(this);
        EventBus.AddListener<IGameFakeVelocityChangeHandler>(this);
    }
    private void OnDestroy()
    {
        EventBus.RemoveListener<IGameBoundsChangeHandler>(this);
        EventBus.RemoveListener<IGameFakeVelocityChangeHandler>(this);
    }
}
