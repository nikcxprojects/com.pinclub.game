using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject _last = null;

    [SerializeField] GameObject menu;
    [SerializeField] GameObject game1;
    [SerializeField] GameObject game2;

    [Space(10)]
    [SerializeField] GameObject backBtnGo;

    public void OpenGame(int id)
    {
        menu.SetActive(false);
        if(id == 0)
        {
            _last = game1;
        }
        else if(id == 1)
        {
            _last = game2;
        }

        _last.SetActive(true);
        backBtnGo.SetActive(true);
    }

    public void Back()
    {
        if(_last)
        {
            _last.SetActive(false);
        }
        
        menu.SetActive(true);
        backBtnGo.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("game");
    }

    public void BackStatus(bool enable)
    {
        backBtnGo.GetComponent<Button>().interactable = enable;
    }
}