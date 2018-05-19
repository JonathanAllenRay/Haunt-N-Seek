using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCloud : MonoBehaviour {

    // Use this for initialization
    public GameplayManager manager;

    private MeshRenderer renderer;
    private GameplayManager.GamePhase currPhase;
	void Start () {
        renderer = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        currPhase = manager.WhichPhase();
        if (currPhase == GameplayManager.GamePhase.P1Hiding || currPhase == GameplayManager.GamePhase.P1Hiding){
            renderer.enabled = true;
        }
        else{
            renderer.enabled = false;   
        }
	}
}
