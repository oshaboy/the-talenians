using UnityEngine;
using System.Collections;
using Noam_Library;
public class FloorMovement : MonoBehaviour {

    public Vector3[] path;
    public float speed;
    public float wait;
    private bool isWaiting = true;
    private int currentPointInPath = 0;
    private float currentPositionInLerp = 0.0f;
    private float stopMoment;
    private int next;



	

	public void Update () {
        if (isWaiting){
            if (Time.time >= stopMoment + wait) {
                isWaiting = false;
            }
        }
        else {
            //Library.UncompoundingLog(currentPointInPath.ToString() + " : " + stopMoment.ToString());
            next = currentPointInPath + 1;
            if (next == path.Length)
            {
                next = 0;
            }
            float distance = Vector3.Distance(path[currentPointInPath], path[next]);
            currentPositionInLerp += (speed * Time.deltaTime) / distance;
            //Library.UncompoundingLog(currentPositionInLerp);
            transform.position = Vector3.Lerp(path[currentPointInPath], path[next], currentPositionInLerp);
            //Library.UncompoundingLog(transform.position);
            if (currentPositionInLerp >= 1)
            {
                currentPointInPath++;
                currentPositionInLerp = 0.0f;
                currentPointInPath = next;
                isWaiting = true;
                stopMoment = Time.time;
            }
        }
	}


    public Vector3 Direction
    {
        get
        {
            if (isWaiting)
                return Vector3.zero;
            else{
                Vector3 intermediateStep = path[next] - path[currentPointInPath];
                intermediateStep.Normalize(); //I must do it this way because normalize returns void
                return speed * intermediateStep;
            }
        }
    }

}