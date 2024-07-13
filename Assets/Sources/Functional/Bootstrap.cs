using UnityEngine;
using UnityEngine.InputSystem;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private PlayerControls _playerControls;

    private PlayerActions _input;

    private void PrepareLevel()
    {
        _input = new PlayerActions();
        _input.Game.Move.performed += (ctx) => { _playerControls.Move = ctx.ReadValue<Vector2>(); };
        _input.Game.Move.canceled += (ctx) => { _playerControls.Move = Vector2.zero; };
        _input.Game.Fire.started += (ctx) => { _playerControls.Fire = true; };
        _input.Game.Fire.canceled += (ctx) => { _playerControls.Fire = false; };

        StartLevel();
    }

    private void StartLevel()
    {
        _input.Game.Enable();
    }

    private void Awake() => PrepareLevel();

    private void OnValidate()
    {
        if (_playerControls == null) { _playerControls = FindObjectOfType<PlayerControls>(); }
    }
}
