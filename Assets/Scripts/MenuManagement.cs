using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagement : MonoBehaviour {

    public GameObject[] screens;
    public List<GameObject> screenStack = new List<GameObject>();

    public GameObject timeToFindText;
    public GameObject timeToPlaceText;
    public GameObject numberOfGhostsText;

    // Use this for initialization
    void Start () {
        // Note: Make sure all panels are active on run or they won't be added to the screen list
		screens = GameObject.FindGameObjectsWithTag("MenuScreen");
        timeToFindText = GameObject.Find("TimeToFindText");
        timeToPlaceText = GameObject.Find("TimeToPlaceText");
        numberOfGhostsText = GameObject.Find("NumberOfGhostsText");
        // Hides all screens except the main menu and sets the main menu to current
        for (int i = 0; i < screens.Length; i++)
        {
            if (screens[i].name != "Main Menu")
            {
                screens[i].SetActive(false);
            }
            else
            {
                screenStack.Add(screens[i]);
            }
        }
    }

    // This is doing roughly the same thing as the name setting
    // in UIBehavior. This is a better way to do it, but I don't 
    // feel like changing the name entry thing right now since it
    // works basically the same. If it has problems later,
    // just change it using this pattern.
    public void changedTimeToPlace(float val)
    {
        StaticVariables.TimeLimitPlace = (int) val;
        timeToPlaceText.GetComponent<UnityEngine.UI.Text>().text = "Time to Hide: " + StaticVariables.TimeLimitPlace + " minutes";
    }

    public void changedTimeToSeek(float val)
    {
        StaticVariables.TimeLimitSeek = (int) val;
        timeToFindText.GetComponent<UnityEngine.UI.Text>().text = "Time to Seek: " + StaticVariables.TimeLimitSeek + " minutes";
    }

    public void changedGhosts(float val)
    {
        StaticVariables.Ghosts = (int) val;
        numberOfGhostsText.GetComponent<UnityEngine.UI.Text>().text = "Ghosts to Hide: " + StaticVariables.Ghosts;
    }

    public void p1ChangeName(string name)
    {
        Debug.Log(name);
        StaticVariables.P1Name = name;
        Debug.Log(StaticVariables.P1Name);
    }

    public void p2ChangeName(string name)
    {
        StaticVariables.P2Name = name;
        Debug.Log(StaticVariables.P2Name);  
    }

    public void mmGoToScreen(string name)
    {
        currentScreen().SetActive(false);
        GameObject obj = getFromScreensList(name);
        if (obj == null)
        {
            Debug.Log("No Screen Found"); 
        }
        screenStack.Add(obj);
        currentScreen().SetActive(true);
    }

    private GameObject getFromScreensList(string name)
    {
        for (int i = 0; i < screens.Length; i++)
        {
            if (screens[i].name == name)
            {
                return screens[i];
            }
        }
        return null;
    }

    public void mmGoBack()
    {
        currentScreen().SetActive(false);
        screenStack.RemoveAt(screenStack.Count - 1);
        currentScreen().SetActive(true);
    }

    private GameObject currentScreen()
    {
        return screenStack[screenStack.Count - 1];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
