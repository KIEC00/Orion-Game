using UnityEngine;

public class ShieldPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var shield = other.gameObject.GetComponentInChildren<Shield>(includeInactive: true);
        if (shield == null) { return; }
        shield.Deploy();
        Destroy(gameObject);
    }
}
