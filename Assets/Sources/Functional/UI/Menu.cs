using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private CanvasGroup _group;
    [SerializeField] private Image _topPanel;
    [SerializeField] private TextMeshProUGUI _highScore;
    [SerializeField] private TextMeshProUGUI _mainText;
    [SerializeField] private Image _bottomPanel;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _startButton;
    [Space]
    [SerializeField] private float _fade;
    [SerializeField] private float _translate;
    [SerializeField] private float _translateExtraOffset;
    [Space]
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _pauseColor;
    [SerializeField] private Color _gameOverColor;

    public void Open(Type type)
    {
        var buttonText = _startButton.GetComponentInChildren<TextMeshProUGUI>();
        switch (type)
        {
            case Type.Start:
                _mainText.color = _startColor;
                _mainText.text = "Orion";
                buttonText.text = "Start";
                _startButton.onClick.AddListener(StartGame);
                break;
            case Type.Pause:
                _mainText.color = _pauseColor;
                _mainText.text = "Pause";
                buttonText.text = "Resume";
                _startButton.onClick.AddListener(Close);
                break;
            case Type.GameOver:
                _mainText.color = _gameOverColor;
                _mainText.text = "Game Over";
                buttonText.text = "Restart";
                _startButton.onClick.AddListener(RestartGame);
                break;
        }
        Pause.Set(true);
        EventBus.Invoke<IMenuHandler>(obj => obj.OnMenuOpen());
        _highScore.text = $"HS {Storage.HighScore:d6}";
        _group.alpha = 0f;
        _topPanel.rectTransform.anchoredPosition = new Vector2(0, _topPanel.rectTransform.sizeDelta.y + _translateExtraOffset);
        _bottomPanel.rectTransform.anchoredPosition = new Vector2(0, -_bottomPanel.rectTransform.sizeDelta.y - _translateExtraOffset);
        _group.gameObject.SetActive(true);
        DOTween.Sequence().SetEase(Ease.InOutCubic).SetUpdate(true)
            .Join(_group.DOFade(1f, _fade))
            .Append(_topPanel.rectTransform.DOAnchorPosY(0f, _translate))
            .Join(_bottomPanel.rectTransform.DOAnchorPosY(0f, _translate))
            .AppendCallback(() => _group.interactable = true);
    }

    public void Close()
    {
        _startButton.onClick.RemoveAllListeners();
        _group.interactable = false;
        DOTween.Sequence().SetEase(Ease.InOutCubic).SetUpdate(true)
            .Join(_topPanel.rectTransform.DOAnchorPosY(_topPanel.rectTransform.sizeDelta.y + _translateExtraOffset, _translate))
            .Join(_bottomPanel.rectTransform.DOAnchorPosY(-_bottomPanel.rectTransform.sizeDelta.y - _translateExtraOffset, _translate))
            .Append(_group.DOFade(0f, _fade))
            .OnComplete(() =>
            {
                _group.gameObject.SetActive(false);
                Pause.Set(false);
                EventBus.Invoke<IMenuHandler>(obj => obj.OnMenuClose());
            });
    }

    private void StartGame()
    {
        Close();
        EventBus.Invoke<ILevelStartHandler>(obj => obj.OnLevelStart());
    }

    private void RestartGame()
    {
        EventBus.Invoke<IRestartHandler>(obj => obj.OnRestart());
    }

    private void Awake() => _quitButton.onClick.AddListener(() => Application.Quit());

    public enum Type { Start, Pause, GameOver }
}
