using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject[] garageObjects;
    public GameObject mainMenu;

    public RectTransform[] menuButtons;

    private void Start()
    {
        menuButtons[0].GetComponent<Button>().onClick.Invoke();
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OpenMainMenu()
    {
        mainMenu.SetActive(true);
        foreach (var item in garageObjects)
        {
            item.SetActive(false);
        }
    }

    public void OpenGarage()
    {
        mainMenu.SetActive(false);
        foreach (var item in garageObjects)
        {
            item.SetActive(true);
        }
    }

    public void MenuButtonInteraction(RectTransform btn)
    {
        foreach (var button in menuButtons)
        {
            if (btn == button)
            {
                button.DOSizeDelta(new Vector2(350f ,button.sizeDelta.y), 0.2f);
                button.GetChild(0).DORotate(new Vector3(0f ,0f, 10f), 0.2f);
                button.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                button.DOSizeDelta(new Vector2(150f, button.sizeDelta.y), 0.2f);
                button.GetChild(0).DORotate(Vector3.zero, 0.2f);
                button.GetChild(1).gameObject.SetActive(false);
            }
        }
    }

}
