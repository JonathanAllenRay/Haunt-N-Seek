using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBehavior : MonoBehaviour {

    private GameObject manager;

    private void Start()
    {
        manager = GameObject.Find("PanelViewManager");       
    }

    public void goToHowToPlay() {
        manager.GetComponent<MenuManagement>().mmGoToScreen("How To Play");
    }

    public void goToSettings()
    {
        manager.GetComponent<MenuManagement>().mmGoToScreen("Settings");
    }

    public void goToInstructions()
    {
        manager.GetComponent<MenuManagement>().mmGoToScreen("Instructions");
    }

    public void goToPreGame()
    {
        manager.GetComponent<MenuManagement>().mmGoToScreen("Pre Game");
    }

    public void goBack()
    {
        manager.GetComponent<MenuManagement>().mmGoBack();
    }

    public void startGame()
    {
        SceneManager.LoadScene("Scenes/Gameplay");
    }

    public void startTutorial()
    {
        SceneManager.LoadScene("Scenes/Tutorial");
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }

}
