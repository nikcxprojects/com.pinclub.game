using System.Collections;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int price;

    public void Drop(Vector2 _position)
    {
        StartCoroutine(DropCard(_position));
    }

    IEnumerator DropCard(Vector2 position)
    {
        float et = 0.0f;
        float dropDuration = 0.25f;

        while(et < dropDuration)
        {
            transform.position = Vector2.Lerp(Vector2.zero, position, et / dropDuration);

            et += Time.deltaTime;
            yield return null;
        }
    }
}
