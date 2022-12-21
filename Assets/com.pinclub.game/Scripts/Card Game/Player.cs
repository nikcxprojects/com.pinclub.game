using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private int count;
    private TextMeshPro countText;

    public bool IsBot;

    private void Awake()
    {
        countText = GetComponentInChildren<TextMeshPro>();
        countText.text = $"{count}";

        CardGameManager.OnCardGet += () =>
        {
            countText.text = $"{++count}";
        };
    }

    public void DropCard(Card card)
    {
        Vector2 position = card.PlayerRef.IsBot ? new Vector2(0, 1.15f) : new Vector2(0, -1.15f);
        card.transform.position = position;
    }
}
