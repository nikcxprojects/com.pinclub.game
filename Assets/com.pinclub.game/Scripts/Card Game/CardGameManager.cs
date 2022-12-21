using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Random = UnityEngine.Random;

public class CardGameManager : MonoBehaviour
{
    public static Action OnCardGet { get; set; }

    private void OnEnable()
    {
        StartGame();
    }

    private void StartGame()
    {
        List<Card> cards = Resources.LoadAll<Card>("Cards").ToList();
        for(int i = 0; i < cards.Count; i++)
        {
            Card tmp = cards[i];
            int rv = Random.Range(i, cards.Count);

            cards[rv] = cards[i];
            cards[i] = tmp;
        }

        StartCoroutine(DropCards(cards));
    }

    private IEnumerator DropCards(List<Card> cards)
    {
        float dropOffset = 0.25f;
        Player[] players = FindObjectsOfType<Player>();

        while(cards.Count > 0)
        {
            for(int i = 0; i < players.Length; i++)
            {
                Card card = Instantiate(cards.Last(), GameObject.Find("deck").transform);
                cards.Remove(card);

                yield return new WaitForSeconds(dropOffset);
            }

            yield return null;
        }
    }
}
