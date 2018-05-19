using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticVariables : MonoBehaviour
{

    private static string p1Name, p2Name;

    // Settings go here
    private static int timeLimitPlace = 2;
    private static int timeLimitSeek = 3;
    private static int ghosts = 3;
    /*
    private static int hints;
    private static int rounds;
    private static int distanceLimit;
    private static bool timeModeActivated;
    */


    public static string P1Name {
        get
        {
            return p1Name;
        }
        set
        {
            p1Name = value;
        }
    }

    public static string P2Name
    {
        get
        {
            return p2Name;
        }
        set
        {
            p2Name = value;
        }
    }

    public static int TimeLimitPlace
    {
        get
        {
            return timeLimitPlace;
        }
        set
        {
            timeLimitPlace = value;
        }
    }


    public static int TimeLimitSeek
    {
        get
        {
            return timeLimitSeek;
        }
        set
        {
            timeLimitSeek = value;
        }
    }


    public static int Ghosts
    {
        get
        {
            return ghosts;
        }
        set
        {
            ghosts = value;
        }
    }




}
