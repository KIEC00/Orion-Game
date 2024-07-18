using TMPro;
using UnityEngine;

public class Hud : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _stageField;
    [SerializeField] TextMeshProUGUI _scoreField;

    public void UpdateStage(string stageName) => _stageField.text = $"Stage {stageName}";
    public void UpdateScore(int score) => _scoreField.text = $"{score:d6}";
}
