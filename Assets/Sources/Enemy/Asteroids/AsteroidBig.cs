using UnityEngine;


public class AsteroidBig : Asteroid
{
    [SerializeField] protected int _maxAsteroidCount = 3;
    [SerializeField] protected float _scaleVariation = 0.5f;
    [SerializeField] private Asteroid _asteroid;

    public new void Destroy()
    {
        var velocity = _rigidbody.velocity;
        base.Destroy();
        InstantiateAsteroids(velocity);
    }

    private void InstantiateAsteroids(Vector2 velocity)
    {
        var count = Random.Range(1, _maxAsteroidCount);
        for (var i = 0; i < count; i++)
        {
            var bounds = _sprite.bounds.size;
            var scale = Random.Range(-_scaleVariation, _scaleVariation) + 1;
            var asteroid = Instantiate(_asteroid, transform.position, Quaternion.identity);
            var rigidbody = asteroid.GetComponent<Rigidbody2D>();
            asteroid.transform.localScale = Vector3.one * scale;
            var maxOffsetAmplitude = (bounds - asteroid.Bounds.size) / 2;
            asteroid.transform.Translate(Random.Range(-maxOffsetAmplitude.x, maxOffsetAmplitude.x), Random.Range(-maxOffsetAmplitude.y, maxOffsetAmplitude.y), 0);
            rigidbody.velocity = velocity;
            rigidbody.rotation = Random.Range(0f, 359f);
        }
    }
}
