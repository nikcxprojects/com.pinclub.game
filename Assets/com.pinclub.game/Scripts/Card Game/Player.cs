using UnityEngine;
using TMPro;
using System;

public class Player : MonoBehaviour
{
    private int count;
    private TextMeshPro countText;

    public bool IsBot;
    public static Action OnCardGet { get; set; }

    private void Awake()
    {
        countText = GetComponentInChildren<TextMeshPro>();
        countText.text = $"{count}";

        OnCardGet += () =>
        {
            countText.text = $"{++count}";
        };
    }
}
