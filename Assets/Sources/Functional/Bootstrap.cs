using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bootstrap : MonoBehaviour, IGameOverPrepareHandler, IGameOverHandler
{
    [SerializeField] private float _fakeVelocity;
    [SerializeField] private PlayerControls _playerControls;
    [SerializeField] private Emitter _emitter;

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
        var playerPosition = new Vector3(gameBounds.xMin - 2, gameBounds.center.y, 0);
        _playerControls.transform.position = playerPosition;
        DOTween.Sequence().SetEase(Ease.OutCubic)
            .Join(_playerControls.transform.DOMoveX(_playerControls.transform.position.x + 10, 1f))
            .AppendCallback(StartLevel);
    }

    private void StartLevel()
    {
        _input.UI.Enable();
        _playerControls.enabled = true;
        _emitter.Run();
    }

    public void OnGameOverPrepare()
    {

    }

    public void OnGameOver()
    {

    }

    private void Start() { PrepareLevel(); }

    private void Awake()
    {
        EventBus.AddListener<IGameOverPrepareHandler>(this);
        EventBus.AddListener<IGameOverHandler>(this);
    }

    private void OnDestroy()
    {
        EventBus.RemoveListener<IGameOverPrepareHandler>(this);
        EventBus.RemoveListener<IGameOverHandler>(this);
    }

    private void OnValidate()
    {
        if (_playerControls == null) { _playerControls = FindObjectOfType<PlayerControls>(); }
    }


}
