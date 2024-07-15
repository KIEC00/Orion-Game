using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour, IDestroyable
{
    [SerializeField] private UnityEvent<Vector2> OnMoveUpdate;
    [SerializeField] private UnityEvent OnFire1Start;
    [SerializeField] private UnityEvent OnFire1Stop;
    [SerializeField] private UnityEvent OnFire2Start;
    [SerializeField] private UnityEvent OnFire2Stop;

    private PlayerActions _input;

    private void Awake()
    {
        _input = new PlayerActions();
        enabled = false;
        _input.Game.Move.performed += (ctx) => OnMoveUpdate.Invoke(ctx.ReadValue<Vector2>());
        _input.Game.Move.canceled += (ctx) => OnMoveUpdate.Invoke(Vector2.zero);
        _input.Game.Fire1.started += (ctx) => OnFire1Start.Invoke();
        _input.Game.Fire1.canceled += (ctx) => OnFire1Stop.Invoke();
        _input.Game.Fire2.started += (ctx) => OnFire2Start.Invoke();
        _input.Game.Fire2.canceled += (ctx) => OnFire2Stop.Invoke();
    }

    private void OnEnable()
    {
        _input.Game.Enable();
    }

    private void OnDisable()
    {
        _input.Game.Disable();
    }

    public void Explode()
    {
        enabled = false;
        EventBus.Invoke<IGameOverPrepareHandler>(obj => obj.OnGameOverPrepare());
    }

    public void Destroy()
    {
        EventBus.Invoke<IGameOverHandler>(obj => obj.OnGameOver());
        Destroy(gameObject);
    }
}
