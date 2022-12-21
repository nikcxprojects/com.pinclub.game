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

    public bool playerStep;

    private IEnumerator BotProcess { get; set; }

    private void Awake()
    {
        CardBackSprite = Resources.Load<Sprite>("CardBackSprite");
    }

    private void OnEnable()
    {
        StartGame();
    }

    private void Update()
    {
        playerStep = PlayerStep;
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
        GameStarted = false;

        Player player = FindObjectsOfType<Player>().OfType<Player>().Where(i => !i.IsBot).First();
        Player bot = FindObjectsOfType<Player>().OfType<Player>().Where(i => i.IsBot).First();

        yield return new WaitForSeconds(0.25f);

        int playerCardPrice =player.DroppedCard.price;
        int botCardPrice = bot.DroppedCard.price;

        player.DroppedCard.Flip(true);
        bot.DroppedCard.Flip(true);

        if (playerCardPrice > botCardPrice)
        {
            yield return StartCoroutine(DropCardToPlayer(player.DroppedCard, player));
            yield return StartCoroutine(DropCardToPlayer(bot.DroppedCard, player));

            bot.DroppedCard.PlayerRef = player;
        }
        else if(botCardPrice > playerCardPrice)
        {
            yield return StartCoroutine(DropCardToPlayer(bot.DroppedCard, bot));
            yield return StartCoroutine(DropCardToPlayer(player.DroppedCard, bot));

            player.DroppedCard.PlayerRef = bot;
        }
        else
        {
            yield return StartCoroutine(DropCardToPlayer(bot.DroppedCard, player));
            yield return StartCoroutine(DropCardToPlayer(player.DroppedCard, bot));

            player.DroppedCard.PlayerRef = bot;
            bot.DroppedCard.PlayerRef = player;
        }

        bool endGame = player.CardCount == 0 || bot.CardCount == 0;
        if(endGame)
        {
            bool IsWin = player.CardCount > 0;
            if(IsWin)
            {
                Instantiate(Resources.Load<WinPopup>("popup"), GameObject.Find("main canvas").transform);
                yield break;
            }
        }

        OnCardGet?.Invoke();
        GameStarted = true;
    }
}
