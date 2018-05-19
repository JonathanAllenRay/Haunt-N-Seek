using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManagerTutorial : MonoBehaviour {


    public GameObject tutorialText;

    private List<GameObject> ghostList = new List<GameObject>();
    public int amtGhosts = 0;

    private bool adding;
    private bool done;


	// Use this for initialization
	void Start () {
        adding = true;
        done = false;
	}

    private void Update()
    {
        if (amtGhosts > 0 && amtGhosts < StaticVariables.Ghosts && adding && !done)
        {
            tutorialText.GetComponent<UnityEngine.UI.Text>().text = "Add " + (StaticVariables.Ghosts - amtGhosts) + " more ghost(s).";
        }
        else if (amtGhosts > 0 && !adding && !done)
        {
            tutorialText.GetComponent<UnityEngine.UI.Text>().text = "Remove " + (amtGhosts) + " more ghost(s).";

        }
    }

    public bool isDone()
    {
        return done;
    }


    public void addGhost(GameObject ghost){
       // ghostList.Add(ghost);
        //FIXME: Add animation for ghost placement(anim, sound, poof, vis to invis)
        amtGhosts++;
        if (adding && amtGhosts >= StaticVariables.Ghosts)
        {
            adding = false;
        }
        Material mat = ghost.GetComponentInChildren<Renderer>().material;
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0.20f);
        mat.shader = Shader.Find("Transparent/Diffuse");
        //Probably don't need this anymore, leaving the code here in case.
        /*if (amtGhosts >= StaticVariables.Ghosts){
            advancePhase();
        }*/
    }
    public bool removeGhost(GameObject ghost){
        amtGhosts--;
        if (!adding && amtGhosts <= 0)
        {
            done = true;
            endText();
        }
        //FIXME: Add animation for ghost discovery(invis to vis, anim, sound, and poof)
        Material mat = ghost.GetComponentInChildren<Renderer>().material;
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 1.0f);
        Destroy(ghost);
        return true;
    }

    private void endText()
    {
        tutorialText.GetComponent<UnityEngine.UI.Text>().text = "This concludes the tutorial.";
    }

    public int getAmtGhosts(){
        return amtGhosts;
    }
	// Update is called once per frame
}
