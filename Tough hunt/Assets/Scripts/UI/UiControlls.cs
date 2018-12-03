using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiControlls : MonoBehaviour {

    [SerializeField]
    private GameObject menuScreen;

    [SerializeField]
    private GameObject optionsScreen;

    public void CloseOptions()
    {
        menuScreen.SetActive(true);
        optionsScreen.SetActive(false);
    }

    public void OpenOptions()
    {
        menuScreen.SetActive(false);
        optionsScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
