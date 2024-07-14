using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemySpaceship : MonoBehaviour, IDestroyable
{
    [SerializeField] Transform _engine;

    public void Explode()
    {
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Animator>().enabled = true;
        Destroy(_engine.gameObject);
    }

    public void Destroy() { Destroy(gameObject); }
}
