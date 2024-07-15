using TMPro;
using UnityEngine;

public class Hud : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _field;

    public void UpdateScore(int score)
    {
        _field.text = $"{score:d6}";
    }
}
