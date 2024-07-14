using System;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour, IDamageable
{
    public float Health { get => _healthPoints; set => _healthPoints = MathF.Max(value, float.MinValue); }

    [SerializeField] private float _healthPoints = 1;
    [SerializeField] private UnityEvent OnDamaged;
    [SerializeField] private UnityEvent OnHealthOver;

    public void ApplyDamage(float damage)
    {
        _healthPoints -= damage;
        if (_healthPoints > 0f)
        {
            OnDamaged.Invoke();
        }
        else
        {
            _healthPoints = 0f;
            OnHealthOver.Invoke();
        }
    }
}
