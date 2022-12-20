using UnityEngine;
using UnityEngine.UI;

public class SoundOption : MonoBehaviour
{
    private bool IsEnable { get; set; } = true;
    [SerializeField] AudioSource source;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            IsEnable = !IsEnable;
            source.mute = !IsEnable;

            GetComponent<Image>().color = IsEnable ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0.25f);
        });

        source.mute = !IsEnable;
        GetComponent<Image>().color = IsEnable ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0.25f);
    }
}
