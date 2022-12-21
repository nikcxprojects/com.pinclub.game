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

            cards[i] = cards[rv];
            cards[rv] = tmp;
        }

        StartCoroutine(DropCards(cards));
    }

    private IEnumerator DropCards(List<Card> cards)
    {
        float dropOffset = 0.1f;
        Player[] players = FindObjectsOfType<Player>();

        while(cards.Count > 0)
        {
            for(int i = 0; i < players.Length; i++)
            {
                Card tmp = cards.Last();

                Card card = Instantiate(tmp, GameObject.Find("deck").transform);
                cards.Remove(tmp);

                StartCoroutine(DropCardToPlayer(card, players[i]));
                yield return new WaitForSeconds(dropOffset);
            }

            yield return null;
        }
    }

    private IEnumerator DropCardToPlayer(Card card, Player player)
    {
        float et = 0.0f;
        float dropDuration = 0.1f;

        while(et < dropDuration)
        {
            card.transform.position = Vector2.Lerp(Vector2.zero, player.transform.position, et / dropDuration);

            et += Time.deltaTime;
            yield return null;
        }

        card.transform.position = player.transform.position;
    }
}
