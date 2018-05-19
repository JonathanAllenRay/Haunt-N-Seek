using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBehaviorGameplay : MonoBehaviour {

    private GameObject manager;

	// Use this for initialization
	void Start () {
        manager = GameObject.Find("GameplayManagerHolder");
	}

    public void advanceToNextPhase()
    {
        manager.GetComponent<GameplayManager>().advancePhase();
    }

    public void resetGame()
    {
        manager.GetComponent<GameplayManager>().resetGame();
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }
}
