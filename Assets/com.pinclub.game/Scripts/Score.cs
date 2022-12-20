using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{
    private void Awake()
    {
        Text textComponent = GetComponent<Text>();
        textComponent.text = $"SCORE: {0}";
    }
}
