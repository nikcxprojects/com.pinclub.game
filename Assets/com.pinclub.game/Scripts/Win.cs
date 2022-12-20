using UnityEngine.UI;
using UnityEngine;

public class Win : MonoBehaviour
{
    private void Awake()
    {
        Text textComponent = GetComponent<Text>();
        Manager.OnEndRolling += (value) =>
        {
            textComponent.text = $"{value}";
        };

        textComponent.text = $"{0}";
    }
}
