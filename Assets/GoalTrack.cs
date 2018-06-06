using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GoalTrack : MonoBehaviour {
    private int counter = 0;
    public int goal;
    public static GoalTrack the_goaltracker;
    public Transform Text;
    public Material activatedMat;
    // Use this for initialization
    public void Start() {
        the_goaltracker = this;
    }

    public void incrementCounter() {
        counter++;
        return;
    }

    public bool didWin()
    {
        return counter >= goal;
    }
    public void win()
    {
        Text.gameObject.SetActive(true);
    }
}
