using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    private TextMeshPro countText;

    public bool IsBot;
    private List<Card> Cards { get; set; } = new List<Card>();

    [HideInInspector]
    public Card DroppedCard = null;

    private void Awake()
    {
        countText = GetComponentInChildren<TextMeshPro>();
        countText.text = $"{Cards.Count}";

        CardGameManager.OnCardGet += () =>
        {
            countText.text = $"{Cards.Count}";
        };
    }

    public void DropCard(Card card)
    {
        DroppedCard = card;

        Vector2 position = card.PlayerRef.IsBot ? new Vector2(0, 1.3f) : new Vector2(0, -1.3f);
        card.transform.position = position;

        card.Flip(false);

        Cards.Remove(card);
        countText.text = $"{Cards.Count}";
    }

    public void AddCard(Card card)
    {
        Cards.Add(card);
    }

    public IEnumerator BotLogic()
    {
        yield return new WaitForSeconds(Random.Range(0.25f, 1.0f));

        Card tmp = Cards[Random.Range(0, Cards.Count)];
        DropCard(tmp);
    }
}
