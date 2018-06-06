using UnityEngine;
using System.Collections;
using Noam_Library;
public class Jump : MonoBehaviour {
	/*

	TODO:

		Make jumps ineffictive on steep slopes

	*/

	public enum State {Jump, Fall, Stand};

	public KeyCode[] jumpButtons;
	public float initialspeed;
	private float speed;
	public float maxHeight;
	public float minHeight;
	public float refreshCanReJumpQMWhenAngleIs;
	private float heightJumped;
	private State state;
    public Transform globalFloor;
	private bool isPressingJump  = false;
	private bool didReleaseJump = true;
	private bool canRejumpqm = true;
	

	private Transform currentFloor;
    public Transform CurrentFloor
    {
        get
        {
            return currentFloor;
        }
    }
	private Vector3 currentNormal;
	//transform r;
	private bool isOnSlopeOrDidJumpFromOne;
	

	//public ArrayList abilities = new ArrayList();
	

	public bool[] getJumpVars () {
        bool[] ans = { isPressingJump, didReleaseJump, canRejumpqm };

        return ans;

	}

	

	public State CurrentState {
        get{
            return state;
        }

	}

	

	public float Speed {
        get{
            return speed;
        }

	}

	public Vector3 CurrentNormal {
        get{
            return currentNormal;
        }

	}

	/*public void Abilities () {
		for (var i = 0; i<abilities.Count; i++)

			(abilities[i] as Special.Ability)();

	}*/

	public State getState () {
		return state;

	}

	public string stringifyState () {
		if (state == State.Jump)

			return "Jump";

		if (state == State.Fall)

			return "Fall";

		if (state == State.Stand)

			return "Stand";

		Debug.LogError("Noam update Stringify State");

		return "Noam update Stringify State";

	}

	public void Start () {
		speed = initialspeed;

		state = State.Fall;

		isPressingJump = false;

		didReleaseJump = !isPressingJump;

		currentNormal = Vector3.down;

	}

	



	public void Update () {
	

		//check if pressing a jump button

		//didReleaseJump = !isPressingJump || didReleaseJump;

		isPressingJump = false;

	    foreach(KeyCode jumpButton in jumpButtons){
			if (Input.GetKey(jumpButton))

			{

	

				isPressingJump = true;

				break;

			}

		}

		if (!isPressingJump){

			didReleaseJump = true;

		}

		

		

		//comment this section later

		var otherwise = true;

		//if it touches floor

		currentFloor = (globalFloor.GetComponent(typeof(Floor)) as Floor).WhatFloorIsColliding(transform);

		if (currentFloor != null){

	

	

			isOnSlopeOrDidJumpFromOne = false;

	

			//var theCollidingFloor= floor.GetComponent(Floor).WhatFloorIsColliding(transform);

			Collision theCollision = (currentFloor.GetComponent(typeof(FloorEverything)) as FloorEverything).getCollision(transform);
			//var collision = currentFloor.GetComponent(FloorEverything).getCollision(transform);

	

	

	

			Vector3 averageNormal = Library.getAvrgNormal(theCollision);
			float AngleToFloor = Library.getAngle(Mathf.Asin(averageNormal.y/averageNormal.magnitude), 2*Mathf.PI, false);
	

			RaycastHit[] allhits;
			currentNormal = -Library.getAvrgNormal(theCollision);

			//Library.UncompoundingLog(-currentNormal);

			//Library.UncompoundingLog("Angle to floor is: " + AngleToFloor.ToString());

			if ( AngleToFloor > (Mathf.PI*1.5-refreshCanReJumpQMWhenAngleIs) && AngleToFloor < (Mathf.PI*1.5+refreshCanReJumpQMWhenAngleIs)){

				//Library.UncompoundingLog(AngleToFloor.ToString() + ", " + refreshCanReJumpQMWhenAngleIs.ToString());

				//reset jumping

				//Debug.Log("hit floor");

	

				/*make sure is on top the floor, not inside it*/

				Ray fromTop = new Ray(transform.position + new Vector3(0.0f,100.0f,0.0f), Vector3.down);
				allhits = Physics.RaycastAll(fromTop);

	            foreach(RaycastHit hit in allhits){
					if (hit.transform == currentFloor && transform.position.y < hit.point.y){

					//	Library.UncompoundingLog("zoom");

						transform.position = hit.point;

						break;

					}

				}

					

				heightJumped = 0.0f;

				//if landed

				//Library.UncompoundingLog(state);

				if (state == State.Fall){

					state = State.Stand;

					canRejumpqm = false;

				}

				//if hit

				if (state == State.Jump){

					state = State.Fall;

	

				}

				//Debug.Log("hit floor");

				otherwise

				 = false;

	

				didReleaseJump = false;	

				//check if player has released the jump button while on the ground

				if (!canRejumpqm && state == State.Stand && !isPressingJump){

					canRejumpqm = true;

				}

	

	

                if (currentFloor.GetComponent(typeof(FloorMovement)) != null)
                {
                    Library.UncompoundingLog((currentFloor.GetComponent(typeof(FloorMovement)) as FloorMovement).Direction * Time.deltaTime);
                    transform.position += (currentFloor.GetComponent(typeof(FloorMovement)) as FloorMovement).Direction * Time.deltaTime;
                } 					

			}

			else {

				Vector3 direction = averageNormal * Time.deltaTime * (globalFloor.GetComponent(typeof(Floor)) as Floor).resistance;
				direction.y = 0;
				transform.position -= direction;
				isOnSlopeOrDidJumpFromOne = true;

	

			}

	

	

	

		}

		//Library.UncompoundingLog(heightJumped);

	//	Debug.Log("currentNormal: " + Library.getVector(currentNormal));

		if ((((heightJumped >= maxHeight || !isPressingJump) || (state == State.Fall || didReleaseJump)) && (heightJumped >= minHeight || (heightJumped == 0 && currentFloor == null) )) || currentNormal.y < 0) {

			//move down

			if (currentNormal.y < 0){

				transform.position += speed * Time.deltaTime * currentNormal;

			}

			else{

				transform.position -= new Vector3(0, speed * Time.deltaTime, 0);

			}

	

			//Debug.Log(speed);

			//if (state != State.Stand){

			//currentNormal.y=-Mathf.Abs(currentNormal.y);

			state = State.Fall;

				//Debug.Log(speed * Time.deltaTime);

	//			Debug.Log("down");

			//}

		}

		//if it is in air and jumping or initiating a jump

		if(((isPressingJump && heightJumped < maxHeight) && canRejumpqm && !didReleaseJump || (heightJumped < minHeight && heightJumped != 0)) && currentNormal.y > 0) {

			//move along the normal

	 		transform.position += speed * Time.deltaTime * currentNormal;

	

			heightJumped += speed * Time.deltaTime;

			//Debug.Log("jumping");

			otherwise = false;

	

			state = State.Jump;

	

	

	//		Debug.Log("up");

			

		}

	

	

	

		//Abilities();

		

	}

	

}