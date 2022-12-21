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
        if(PlayerRef.IsBot || !CardGameManager.GameStarted || !CardGameManager.PlayerStep)
        {
            return;
        }

        PlayerRef.DropCard(this);
        CardGameManager.PlayerStep = false;
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
