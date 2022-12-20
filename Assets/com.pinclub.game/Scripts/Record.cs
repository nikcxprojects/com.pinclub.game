using UnityEngine.UI;
using UnityEngine;

public class Record : MonoBehaviour
{
    private const string key = "record";
    private int Count
    {
        get => PlayerPrefs.GetInt(key, 0);
        set => PlayerPrefs.SetInt(key, value);
    }

    private void Awake()
    {
        Text textComponent = GetComponent<Text>();
        textComponent.text = $"SCORE: {Count}";
    }
}
