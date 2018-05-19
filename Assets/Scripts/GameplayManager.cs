using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour {

    public GameObject EndScreen;
    public GameObject PreTurnScreen;
    public GameObject EndRoundScreen;
    public GameObject UIScreen;

    public GameObject textTopPreTurn;
    public GameObject textSecondPreTurn;
    public GameObject textButtonPreTurn;

    public GameObject textMidRound;

    public GameObject textWinrar30DayTrial;
    public GameObject textScoreboardEnd;

    public GameObject textTimer;
    public GameObject textGhostsBro;

    public GameObject nextButton;

    public GameObject camera;

    public AudioSource sfxDing;
    public AudioSource poof;

    public GameObject poofPart;
    //Time management
    private float timeLeft;
    private float attenConstant = 1.5f;

    private GamePhase currentPhase;
    private int hideTime;
    private int seekTime;
    private List<GameObject> ghostList = new List<GameObject>();
    private List<float> ghostDistances = new List<float>();


    public int amtGhosts = 0;
    private int p1Score = 0;
    private int p2Score = 0;

    public enum GamePhase
    {
        Player1Hide, P1Hiding, Player2Seek, P2Seeking, EndRound1, Player2Hide, P2Hiding, Player1Seek,
        P1Seeking, EndRound2, EndScreen
    };

    public GamePhase WhichPhase(){
        return currentPhase;
    }

	// Use this for initialization
	void Start () {
        PreTurnScreen = GameObject.Find("PreTurnScreen");
        EndScreen = GameObject.Find("EndScreen");
        EndRoundScreen = GameObject.Find("EndRoundScreen");
        UIScreen = GameObject.Find("UI");
        UIScreen.SetActive(false);
        EndRoundScreen.SetActive(false);
        EndScreen.SetActive(false);
        currentPhase = GamePhase.Player1Hide;
        hideTime = StaticVariables.TimeLimitPlace * 60;
        seekTime = StaticVariables.TimeLimitSeek * 60;
        if (StaticVariables.P1Name == null)
        {
            StaticVariables.P1Name = "Player 1";
        }
        if (StaticVariables.P2Name == null)
        {
            StaticVariables.P2Name = "Player 2";
        }
        updatePhase();
	}

    void CalculateDistances(){
        ghostDistances.Clear();
        float maxDist = -1.0f;
        foreach (GameObject ghost in ghostList){
            float dist = Vector3.Distance(ghost.transform.position, camera.transform.position);
            if (dist > maxDist){
                maxDist = dist;
            }
            ghostDistances.Add(dist);
        }
        int count = 0;
        foreach (GameObject ghost in ghostList)
        {
            Material mat = ghost.GetComponentInChildren<Renderer>().material;
            float alpha = attenConstant*(1 - (ghostDistances[count]));
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, Mathf.Clamp(alpha, 0.0f, 1.0f));
            count++;
        }
    }

    private void Update()
    {
        if (timerActive())
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                textTimer.GetComponent<UnityEngine.UI.Text>().text = "Outta Time";
                advancePhase();
                sfxDing.Play();
            }
            int roundedSeconds = (int) timeLeft;
            textTimer.GetComponent<UnityEngine.UI.Text>().text = "Time Left: " + roundedSeconds;
        }
        if ((currentPhase == GamePhase.P1Hiding || currentPhase == GamePhase.P2Hiding) && amtGhosts >= StaticVariables.Ghosts)
        {
            nextButton.SetActive(true);
        }
        else
        {
            nextButton.SetActive(false);
        }
        if (currentPhase == GamePhase.P1Seeking || currentPhase == GamePhase.P2Seeking){
            CalculateDistances();
        }
    }


    public void poofFunc(Vector3 loc, Quaternion rot)
    {
        Instantiate(poofPart, loc, rot);
    }

    void setupPlayer1Hide()
    {
        clearGhosts();
        textTopPreTurn.GetComponent<UnityEngine.UI.Text>().text = StaticVariables.P1Name + "'s\nturn to hide.";
        textSecondPreTurn.GetComponent<UnityEngine.UI.Text>().text = "Pass the phone to\n" + StaticVariables.P1Name + ".";
    }

    void setupPlayer2Seek()
    {
        textTopPreTurn.GetComponent<UnityEngine.UI.Text>().text = StaticVariables.P2Name + "'s\nturn to seek.";
        textSecondPreTurn.GetComponent<UnityEngine.UI.Text>().text = "Pass the phone to\n" + StaticVariables.P2Name + ".";
    }
    
    void setupEndRound1()
    {
        p2Score = StaticVariables.Ghosts - amtGhosts;
        textMidRound.GetComponent<UnityEngine.UI.Text>().text = StaticVariables.P2Name + " found " + p2Score + " ghosts.";
    }

    void setupEndRound2()
    {
        p1Score = StaticVariables.Ghosts - amtGhosts;
        textMidRound.GetComponent<UnityEngine.UI.Text>().text = StaticVariables.P1Name + " found " + p1Score + " ghosts.";
    }

    void setupPlayer2Hide()
    {
        clearGhosts();
        textTopPreTurn.GetComponent<UnityEngine.UI.Text>().text = StaticVariables.P2Name + "'s\nturn to hide.";
        textSecondPreTurn.GetComponent<UnityEngine.UI.Text>().text = "Pass the phone to\n" + StaticVariables.P2Name + ".";
    }

    void setupPlayer1Seek()
    {
        textTopPreTurn.GetComponent<UnityEngine.UI.Text>().text = StaticVariables.P1Name + "'s\nturn to seek.";
        textSecondPreTurn.GetComponent<UnityEngine.UI.Text>().text = "Pass the phone to\n" + StaticVariables.P1Name + ".";
    }

    void setupEndScreen()
    {
        if (p1Score > p2Score)
        {
            textWinrar30DayTrial.GetComponent<UnityEngine.UI.Text>().text = StaticVariables.P1Name + " wins!";
        }
        else if (p2Score > p1Score){
            textWinrar30DayTrial.GetComponent<UnityEngine.UI.Text>().text = StaticVariables.P2Name + " wins!";
        }
        else{
            textWinrar30DayTrial.GetComponent<UnityEngine.UI.Text>().text = "Tie!";
        }
        textScoreboardEnd.GetComponent<UnityEngine.UI.Text>().text = StaticVariables.P1Name + ": " + p1Score +  "\n" + StaticVariables.P2Name + ": " + p2Score;
    }

    public void advancePhase()
    {
        currentPhase++;
        updatePhase();
    }

    public void updatePhase()
    {
        //Nothing yet
        if (currentPhase == GamePhase.Player1Hide)
        {
            PreTurnScreen.SetActive(true);
            EndScreen.SetActive(false);
            EndRoundScreen.SetActive(false);
            UIScreen.SetActive(false);
            setupPlayer1Hide();
            // Resetting this is probably redundant, but doing it to be safe
            p1Score = 0;
            p2Score = 0;
        }
        else if (currentPhase == GamePhase.P1Hiding)
        {
            PreTurnScreen.SetActive(false);
            UIScreen.SetActive(true);
            textGhostsBro.GetComponent<UnityEngine.UI.Text>().text = "Ghosts To Place: " + amtGhosts + "/" + StaticVariables.Ghosts;
            timeLeft = StaticVariables.TimeLimitPlace * 60;
        }
        else if (currentPhase == GamePhase.Player2Seek)
        {
            PreTurnScreen.SetActive(true);
            setupPlayer2Seek();
            UIScreen.SetActive(false);
        }
        else if (currentPhase == GamePhase.P2Seeking)
        {
            PreTurnScreen.SetActive(false);
            UIScreen.SetActive(true);
            textGhostsBro.GetComponent<UnityEngine.UI.Text>().text = "Ghosts Remaining: " + amtGhosts + "/" + StaticVariables.Ghosts;
            timeLeft = StaticVariables.TimeLimitSeek * 60;
        }
        else if (currentPhase == GamePhase.EndRound1)
        {
            EndRoundScreen.SetActive(true);
            setupEndRound1();
            UIScreen.SetActive(false);
        }
        else if (currentPhase == GamePhase.Player2Hide)
        {
            EndRoundScreen.SetActive(false);
            PreTurnScreen.SetActive(true);
            setupPlayer2Hide();
        }
        else if (currentPhase == GamePhase.P2Hiding)
        {
            PreTurnScreen.SetActive(false);
            UIScreen.SetActive(true);
            textGhostsBro.GetComponent<UnityEngine.UI.Text>().text = "Ghosts To Place: " + amtGhosts + "/" + StaticVariables.Ghosts;
            timeLeft = StaticVariables.TimeLimitPlace * 60;

        }
        else if (currentPhase == GamePhase.Player1Seek)
        {
            PreTurnScreen.SetActive(true);
            setupPlayer1Seek();
            UIScreen.SetActive(false);
        }
        else if (currentPhase == GamePhase.P1Seeking)
        {
            PreTurnScreen.SetActive(false);
            UIScreen.SetActive(true);
            textGhostsBro.GetComponent<UnityEngine.UI.Text>().text = "Ghosts Remaining: " + amtGhosts + "/" + StaticVariables.Ghosts;
            timeLeft = StaticVariables.TimeLimitSeek * 60;
        }
        else if (currentPhase == GamePhase.EndRound2)
        {
            EndRoundScreen.SetActive(true);
            setupEndRound2();
            UIScreen.SetActive(false);
        }
        else
        {
            EndRoundScreen.SetActive(false);
            EndScreen.SetActive(true);
            setupEndScreen();
        }
    }

    public void resetGame()
    {
        currentPhase = GamePhase.Player1Hide;
        updatePhase();
    }

    private void clearGhosts()
    {
        amtGhosts = 0;
        ghostList.Clear();
    }
	
    public void addGhost(GameObject ghost){
       // ghostList.Add(ghost);
        //FIXME: Add animation for ghost placement(anim, sound, poof, vis to invis)
        amtGhosts++;
        poof.Play();
        Material mat = ghost.GetComponentInChildren<Renderer>().material;
        mat.shader = Shader.Find("Transparent/Diffuse");
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0.5f);
        ghostList.Add(ghost);
        //Probably don't need this anymore, leaving the code here in case.
        /*if (amtGhosts >= StaticVariables.Ghosts){
            advancePhase();
        }*/
        textGhostsBro.GetComponent<UnityEngine.UI.Text>().text = "Ghosts To Place: " + amtGhosts + "/" + StaticVariables.Ghosts;
    }
    public bool removeGhost(GameObject ghost){
        if (currentPhase == GamePhase.P1Seeking || currentPhase == GamePhase.P2Seeking)
        {
            amtGhosts--;
            poof.Play();
            //FIXME: Add animation for ghost discovery(invis to vis, anim, sound, and poof)
            Material mat = ghost.GetComponentInChildren<Renderer>().material;
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 1.0f);
            ghostList.Remove(ghost);
            Destroy(ghost);
            textGhostsBro.GetComponent<UnityEngine.UI.Text>().text = "Ghosts Remaining: " + ghostList.Count;
            if (amtGhosts <= 0)
            {
                advancePhase();
            }
        }
        return true;
    }

    private bool timerActive()
    {
        if (currentPhase == GamePhase.P1Hiding || currentPhase == GamePhase.P1Seeking ||
            currentPhase == GamePhase.P2Hiding || currentPhase == GamePhase.P2Seeking)
        {
            return true;
        }
        return false;
    }

    public int getAmtGhosts(){
        return amtGhosts;
    }
	// Update is called once per frame
}
