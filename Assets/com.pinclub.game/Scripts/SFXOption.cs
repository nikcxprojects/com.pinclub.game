using UnityEngine;
using UnityEngine.UI;

public class SFXOption : MonoBehaviour
{
    private bool IsEnable { get; set; } = true;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            IsEnable = !IsEnable;
            GetComponent<Image>().color = IsEnable ? new Color(1,1,1,0.25f) : new Color(1, 1, 1, 1);
        });

        GetComponent<Image>().color = IsEnable ? new Color(1, 1, 1, 0.25f) : new Color(1, 1, 1, 1);
    }
}
