using System.Collections;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int price;

    private SpriteRenderer Renderer { get; set; }
    private (Sprite face, Sprite back) CardData { get; set; }
    public Player PlayerRef { get; set; }

    private void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if(PlayerRef.IsBot || !CardGameManager.GameStarted)
        {
            return;
        }

        DropCard();
        Flip(false);
    }

    public void DropCard()
    {
        transform.position = PlayerRef.IsBot ? new Vector2(0, 1.15f) : new Vector2(0, -1.15f);
    }

    public void Flip(bool IsHide)
    {
        Renderer.sprite = IsHide ? CardData.back : CardData.face;
    }

    public void SetCardData(Sprite face, Sprite back, Player playerRef)
    {
        CardData = (face, back);
        PlayerRef = playerRef;
    }
}
