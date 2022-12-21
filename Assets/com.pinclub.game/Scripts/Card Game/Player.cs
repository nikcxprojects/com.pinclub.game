using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    private int count;
    private TextMeshPro countText;

    public bool IsBot;
    private List<Card> Cards { get; set; } = new List<Card>();

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

    public void AddCard(Card card)
    {
        Cards.Add(card);
    }

    public IEnumerator BotLogic()
    {
        yield return new WaitForSeconds(Random.Range(0.25f, 1.0f));

        Card tmp = Cards[Random.Range(0, Cards.Count)];

        DropCard(tmp);
        Cards.Remove(tmp);
    }
}
