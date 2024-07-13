using UnityEngine;

public class PlayerControls : MonoBehaviour, IControllable
{
    public Vector2 Move { get; set; }
    public bool Fire { get; set; }
}
