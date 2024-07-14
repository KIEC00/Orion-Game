using UnityEngine;


public class AsteroidBig : Asteroid
{
    [SerializeField] protected int _maxAsteroidCount = 3;
    [SerializeField] protected float _randomMinScale = 0.5f;
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
            var offset = new Vector2(Random.Range(-bounds.x / 2, bounds.x / 2), Random.Range(-bounds.y / 2, bounds.y / 2));
            var asteroid = Instantiate(_asteroid, transform.position + (Vector3)offset, Quaternion.identity);
            var scale = Random.Range(_randomMinScale, 1f);
            var rigidbody = asteroid.GetComponent<Rigidbody2D>();
            asteroid.transform.localScale = Vector3.one * scale;
            rigidbody.velocity = velocity;
            rigidbody.rotation = Random.Range(0f, 359f);
        }
    }
}
