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
            GetComponent<Image>().color = IsEnable ? Color.red : Color.green;
        });

        GetComponent<Image>().color = IsEnable ? Color.red : Color.green;
    }
}
