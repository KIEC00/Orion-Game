using UnityEngine;
using UnityEngine.InputSystem;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private PlayerControls _playerControls;

    private PlayerActions _input;

    private void PrepareLevel()
    {
        _input = new PlayerActions();
        // _input.UI.Menu += (ctx) => ; 
        // StartLevel();
    }

    private void StartLevel()
    {
        _input.UI.Enable();
        _playerControls.enabled = true;

    }

    private void Awake() => PrepareLevel();
    private void Start() => StartLevel();

    private void OnValidate()
    {
        if (_playerControls == null) { _playerControls = FindObjectOfType<PlayerControls>(); }
    }
}
