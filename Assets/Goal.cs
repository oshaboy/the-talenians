using UnityEngine;
using System.Collections;
using Noam_Library;
//using GoalTracker;
public class Goal : MonoBehaviour
{
    public Transform cur_player;
    private bool didActivate = false;
    public Transform WinText;

    public bool DidActivate
    {
        get
        {
            return didActivate;
        }
    }

    public void OnCollisionEnter(Collision collision) {
       if (collision.transform == cur_player && !didActivate){
            didActivate = true;
            (GetComponent(typeof(Renderer)) as Renderer).material = GoalTrack.the_goaltracker.activatedMat;
            GoalTrack.the_goaltracker.incrementCounter();
            if (GoalTrack.the_goaltracker.didWin()) {
                GoalTrack.the_goaltracker.win();
            }

       }
    }

}
