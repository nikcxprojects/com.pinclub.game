using UnityEngine.UI;
using UnityEngine;

public class VibraOption : MonoBehaviour
{
    public static bool IsEnable { get; set; } = true;

    [SerializeField] Sprite active;
    [SerializeField] Sprite inactive;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            IsEnable = !IsEnable;
            GetComponent<Image>().sprite = IsEnable ? active : inactive;
        });
    }
}
