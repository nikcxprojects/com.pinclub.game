using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameManager : MonoBehaviour
{
    private void OnEnable()
    {
        StartGame();
    }

    private void StartGame()
    {
        Card[] cards = Resources.LoadAll<Card>("Cards");
        for(int i = 0; i < cards.Length; i++)
        {
            Card tmp = cards[i];
            int rv = Random.Range(i, cards.Length);

            cards[rv] = cards[i];
            cards[i] = tmp;
        }

        for(int i = 0; i < cards.Length; i++)
        {
            Instantiate(cards[i], GameObject.Find("deck").transform);
        }
    }
}
