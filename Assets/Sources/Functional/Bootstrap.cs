using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-100)]
public class Bootstrap : MonoBehaviour, ILevelStartHandler, IMenuHandler, IGameOverPrepareHandler, IGameOverHandler
{
    [SerializeField] private float _fakeVelocity;
    [SerializeField] private PlayerControls _playerControls;
    [SerializeField] private Emitter _emitter;
    [SerializeField] private Menu _menu;

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
        _input.UI.Menu.performed += (ctx) => {_menu.Open(Menu.Type.Pause); };

        var playerPosition = new Vector3(gameBounds.xMin - 2, gameBounds.center.y, 0);
        _playerControls.transform.position = playerPosition;
        DOTween.Sequence().SetEase(Ease.OutCubic)
            .Join(_playerControls.transform.DOMoveX(_playerControls.transform.position.x + 10, 1f))
            .AppendCallback(() => _menu.Open(Menu.Type.Start));
    }

    public void OnLevelStart()
    {
        _emitter.Run();
    }

    public void OnGameOverPrepare()
    {
        DisableInput();
    }

    public void OnGameOver()
    {
        _menu.Open(Menu.Type.GameOver);
    }

    private void EnableInput()
    {
        _input.UI.Enable();
        _input.Enable();
        _playerControls.enabled = true;
    }

    private void DisableInput()
    {
        _input.UI.Disable();
        _playerControls.enabled = false;
    }

    public void OnMenuOpen()
    {
        DisableInput();
    }

    public void OnMenuClose()
    {
        EnableInput();
    }

    private void Start() { PrepareLevel(); }

    private void Awake()
    {
        Pause.Set(false);
        EventBus.AddListener<ILevelStartHandler>(this);
        EventBus.AddListener<IMenuHandler>(this);
        EventBus.AddListener<IGameOverPrepareHandler>(this);
        EventBus.AddListener<IGameOverHandler>(this);
    }

    private void OnDestroy()
    {
        EventBus.RemoveListener<ILevelStartHandler>(this);
        EventBus.RemoveListener<IMenuHandler>(this);
        EventBus.RemoveListener<IGameOverPrepareHandler>(this);
        EventBus.RemoveListener<IGameOverHandler>(this);
    }

    private void OnValidate()
    {
        if (_playerControls == null) { _playerControls = FindObjectOfType<PlayerControls>(); }
    }
}
