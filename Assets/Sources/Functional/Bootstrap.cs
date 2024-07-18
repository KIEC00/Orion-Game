using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)]
public class Bootstrap : MonoBehaviour, ILevelStartHandler, IMenuHandler, IGameOverPrepareHandler, IGameOverHandler, IRestartHandler
{
    [SerializeField] private float _fakeVelocity;
    [SerializeField] private PlayerControls _playerControls;
    [SerializeField] private Emitter _emitter;
    [SerializeField] private Menu _menu;
    [SerializeField] private ScoreCounter _scoreCounter;

    private PlayerActions _input;
    private static bool _isFirstStart = true;

    private void PrepareLevel()
    {
        var camera = Camera.main;
        var cameraHeight = camera.orthographicSize * 2;
        var cameraSize = new Vector2(camera.aspect * cameraHeight, cameraHeight);
        var gameBounds = new Rect((Vector2)camera.transform.position - cameraSize / 2, cameraSize);

        EventBus.Invoke<IGameBoundsChangeHandler>(obj => obj.OnGameBoundsChange(gameBounds));
        EventBus.Invoke<IGameFakeVelocityChangeHandler>(obj => obj.OnFakeVelocityChange(_fakeVelocity));

        _input = new PlayerActions();
        _input.UI.Menu.performed += (ctx) => { _menu.Open(Menu.Type.Pause); };

        SceneTransition.Instance.InstantShow();
        SceneTransition.Instance.Hide();

        var playerPosition = new Vector3(gameBounds.xMin - 2, gameBounds.center.y, 0);
        _playerControls.transform.position = playerPosition;
        DOTween.Sequence().SetEase(Ease.OutCubic)
            .Join(_playerControls.transform.DOMoveX(_playerControls.transform.position.x + 10, 1f))
            .AppendCallback(OnLevelReady);
    }

    private void OnLevelReady()
    {
        if (_isFirstStart) { _menu.Open(Menu.Type.Start); _isFirstStart = false; }
        else { EnableInput(); EventBus.Invoke<ILevelStartHandler>(obj => obj.OnLevelStart()); }
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
        Storage.HighScore = Math.Max(_scoreCounter.Score, Storage.HighScore);
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

    public void OnRestart()
    {
        SceneTransition.Instance.Show(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    private void Start() { PrepareLevel(); }

    private void Awake()
    {
        Pause.Set(false);
        EventBus.AddListener<ILevelStartHandler>(this);
        EventBus.AddListener<IMenuHandler>(this);
        EventBus.AddListener<IGameOverPrepareHandler>(this);
        EventBus.AddListener<IGameOverHandler>(this);
        EventBus.AddListener<IRestartHandler>(this);
    }

    private void OnDestroy()
    {
        EventBus.RemoveListener<ILevelStartHandler>(this);
        EventBus.RemoveListener<IMenuHandler>(this);
        EventBus.RemoveListener<IGameOverPrepareHandler>(this);
        EventBus.RemoveListener<IGameOverHandler>(this);
        EventBus.RemoveListener<IRestartHandler>(this);
    }

    private void OnValidate()
    {
        if (_playerControls == null) { _playerControls = FindObjectOfType<PlayerControls>(); }
    }
}
