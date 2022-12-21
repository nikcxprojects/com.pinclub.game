using System.Collections;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int price;

    private SpriteRenderer Renderer { get; set; }
    private (Sprite face, Sprite back) CardData { get; set; }

    private void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
    }

    public void Flip(bool IsHide)
    {
        Renderer.sprite = IsHide ? CardData.back : CardData.face;
    }

    public void SetCardData(Sprite face, Sprite back)
    {
        CardData = (face, back);
    }
}
