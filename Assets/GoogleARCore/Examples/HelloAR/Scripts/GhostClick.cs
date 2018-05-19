using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostClick : MonoBehaviour {

    public GameplayManager manager;
    private string gameplayManagerName = "GameplayManagerHolder";
	// Use this for initialization
	void Start () {
        manager = GameObject.Find(gameplayManagerName).GetComponent<GameplayManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
      //  Debug.Log("Hit");
        manager.removeGhost(gameObject);
        manager.poofFunc(gameObject.transform.position, gameObject.transform.rotation);
        //  Destroy(gameObject);
    }
}
