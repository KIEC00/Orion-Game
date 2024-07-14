using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private UnityEvent<Vector2> OnMoveUpdate;
    [SerializeField] private UnityEvent OnFireStart;
    [SerializeField] private UnityEvent OnFireStop;

    private PlayerActions _input;

    private void Awake()
    {
        _input = new PlayerActions();
        enabled = false;
        _input.Game.Move.performed += (ctx) => OnMoveUpdate.Invoke(ctx.ReadValue<Vector2>());
        _input.Game.Move.canceled += (ctx) => OnMoveUpdate.Invoke(Vector2.zero);
        _input.Game.Fire.started += (ctx) => OnFireStart.Invoke();
        _input.Game.Fire.canceled += (ctx) => OnFireStop.Invoke();
    }

    private void OnEnable()
    {
        _input.Game.Enable();
    }

    private void OnDisable()
    {
        _input.Game.Disable();
    }
}
