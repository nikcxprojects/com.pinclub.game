using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private int count;
    private TextMeshPro countText;

    public bool IsBot;

    private void Awake()
    {
        countText = GetComponent<TextMeshPro>();
        CardGameManager.OnCardGet += () =>
        {
            countText.text = $"{++count}";
        };
    }
}
