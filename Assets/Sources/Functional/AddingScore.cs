using UnityEngine;

public class AddingScore : MonoBehaviour
{
    [SerializeField] private int _fixedScore;
    public void AddFixed() => Add(_fixedScore);
    public void Add(int score) => EventBus.Invoke<IScoreAddHandler>(obj => obj.OnScoreAdd(score));
}
