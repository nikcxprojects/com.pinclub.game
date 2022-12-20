using System.Collections;
using UnityEngine;

using Random = UnityEngine.Random;

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
            Card card = Instantiate(cards[i], GameObject.Find("deck").transform);
        }
    }
}
