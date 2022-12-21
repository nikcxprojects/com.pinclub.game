using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Random = UnityEngine.Random;

public class CardGameManager : MonoBehaviour
{
    public static bool PlayerStep { get; set; }
    public static Action OnCardGet { get; set; }
    private Sprite CardBackSprite { get; set; }
    public static bool GameStarted { get; set; }

    private IEnumerator BotProcess { get; set; }

    private void Awake()
    {
        CardBackSprite = Resources.Load<Sprite>("CardBackSprite");
    }

    private void OnEnable()
    {
        StartGame();
    }

    private void StartGame()
    {
        GameStarted = false;
        PlayerStep = false;

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
                Sprite cardFaceSprite = card.GetComponent<SpriteRenderer>().sprite;

                card.SetCardData(cardFaceSprite, CardBackSprite, players[i]);
                card.Flip(true);

                cards.Remove(tmp);

                card.StartCoroutine(DropCardToPlayer(card, players[i]));
                yield return new WaitForSeconds(dropOffset);
            }

            OnCardGet?.Invoke();
            yield return null;
        }

        StartCoroutine(nameof(GameProcess));
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
        player.AddCard(card);
    }

    private IEnumerator GameProcess()
    {
        GameStarted = true;

        Player player = FindObjectsOfType<Player>().OfType<Player>().Where(i => !i.IsBot).First();
        Player bot = FindObjectsOfType<Player>().OfType<Player>().Where(i => i.IsBot).First();

        BotProcess = bot.BotLogic();

        while (true)
        {
            yield return BotProcess;
            PlayerStep = true;

            while (PlayerStep)
            {
                yield return null;
            }

            yield return StartCoroutine(nameof(CheckResult));
            BotProcess = bot.BotLogic();
        }
    }

    private IEnumerator CheckResult()
    {
        Player player = FindObjectsOfType<Player>().OfType<Player>().Where(i => !i.IsBot).First();
        Player bot = FindObjectsOfType<Player>().OfType<Player>().Where(i => i.IsBot).First();

        yield return new WaitForSeconds(0.25f);

        Debug.Log(player.DroppedCard.price);
        Debug.Log(bot.DroppedCard.price);

        Destroy(player.DroppedCard.gameObject);
        Destroy(bot.DroppedCard.gameObject);
    }
}
