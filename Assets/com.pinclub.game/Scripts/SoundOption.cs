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

            GetComponent<Image>().color = IsEnable ? Color.green : Color.red;
        });

        source.mute = !IsEnable;
        GetComponent<Image>().color = IsEnable ? Color.green : Color.red;
    }
}
