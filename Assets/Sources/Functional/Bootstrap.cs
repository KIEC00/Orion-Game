using UnityEngine;
using UnityEngine.InputSystem;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private float _fakeVelocity;
    [SerializeField] private PlayerControls _playerControls;

    private PlayerActions _input;

    private void PrepareLevel()
    {
        var camera = Camera.main;
        var cameraHeight = camera.orthographicSize * 2;
        var cameraSize = new Vector2(camera.aspect * cameraHeight, cameraHeight);
        var gameBounds = new Rect((Vector2)camera.transform.position - cameraSize / 2, cameraSize);

        EventBus.Invoke<IGameBoundsChangeHandler>(obj => obj.OnGameBoundsChange(gameBounds));
        EventBus.Invoke<IGameFakeVelocityChangeHandler>(obj => obj.OnFakeVelocityChange(_fakeVelocity));

        _input = new PlayerActions();
        // _input.UI.Menu += (ctx) => ; 
        // StartLevel();
    }

    private void StartLevel()
    {
        _input.UI.Enable();
        _playerControls.enabled = true;

    }

    private void Start() { PrepareLevel(); StartLevel(); }

    private void OnValidate()
    {
        if (_playerControls == null) { _playerControls = FindObjectOfType<PlayerControls>(); }
    }
}
