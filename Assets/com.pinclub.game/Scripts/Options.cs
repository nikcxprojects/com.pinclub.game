using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    private bool expanded;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            expanded = !expanded;
            transform.GetChild(0).gameObject.SetActive(expanded);
        });
    }
}
