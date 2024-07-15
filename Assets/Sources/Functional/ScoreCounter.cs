using UnityEngine;
using UnityEngine.Events;

public class ScoreCounter : MonoBehaviour, IScoreAddHandler
{
    public int Score => _score;
    public UnityEvent<int> UpdateScore;

    private int _score = 0;
    public void OnScoreAdd(int count) { _score += count; UpdateScore.Invoke(_score); }

    private void Awake() => EventBus.AddListener<IScoreAddHandler>(this);
    private void Start() => UpdateScore.Invoke(_score);
    private void OnDestroy() => EventBus.RemoveListener<IScoreAddHandler>(this);
}
