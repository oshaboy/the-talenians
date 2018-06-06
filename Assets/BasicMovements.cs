using UnityEngine;
using System.Collections;
using Noam_Library;
public class BasicMovements : MonoBehaviour {
	public KeyCode[] left;
	public KeyCode[] right;
    public KeyCode[] up;
	public KeyCode[] down;
    public Camera thirdPersonCamera;
	public float floorspeed;
	public float airspeed;
	//public float scaleHeight;
    public float scaleAngle;
    //private Vector3 scaleHeightAsVector;
    public float xadjust;
    public float yadjust;
	private Vector3 currentAxisOfPlayerQuaternion;
	private float currentAngleOfPlayerQuaternion;
    public ArrayList abilities = new ArrayList();
    //public bool isStandingOnFloor = false;



    /*public void UseAbilities () {
		for (var i = 0; i<abilities.Count; i++)

			(abilities[i] as Special.Ability)();

	}*/

	public void Start () {
		//scaleHeightAsVector = new Vector3(0,scaleHeight,0);

		(thirdPersonCamera.GetComponent(typeof(CameraScript)) as CameraScript).character = transform;

		currentAxisOfPlayerQuaternion = Vector3.up;

		currentAngleOfPlayerQuaternion = 0;
    }

	

	public void BuffSpeed (float amount) {
		airspeed *=amount;

		floorspeed *= amount;

	}

	public void Walk (Vector3 direction) {
		direction.y = 0;

		direction.Normalize();

		//get the floor the player is colliding with

		Transform TheFloor = Globals.transformTable["Floor"] as Transform;
		var floorscript = TheFloor.GetComponent(typeof(Floor)) as Floor;

		var currentFloorPlayerIsHitting = floorscript.WhatFloorIsColliding(transform);

		//Vector3 SlopeNormal = transform.GetComponent(Jump).SlopeNormal;
		//Vector3 EulerSave = transform.eulerAngles;
		//float angle;
		if (direction.magnitude != 0){

			if (direction.x > 0){

				//Library.UncompoundingLog("direction.x is positive");

				currentAngleOfPlayerQuaternion = -Mathf.Asin(direction.z/direction.magnitude) - Mathf.PI/2.0f;

			}

			else{

				currentAngleOfPlayerQuaternion = Mathf.Asin(direction.z/direction.magnitude) + (Mathf.PI/2.0f);

			}

			// Library.UncompoundingLog(currentAngleOfPlayerQuaternion);

		}

		if (currentFloorPlayerIsHitting != null){ //if it is hitting a floor

	

			/*

			find if the next position can be scaled and move the y position there

			*/

			var futureposition = transform.position + direction*floorspeed*Time.deltaTime; 

			Ray ray = new Ray(futureposition, Vector3.up);
			RaycastHit[] allhits = Physics.RaycastAll(ray);

	foreach(RaycastHit hit in allhits){
	

			//Debug.Log(transform.GetComponent(Jump).stringifyState());

				if (hit.transform.Equals(currentFloorPlayerIsHitting)){ //if it is the same floor currently stood on

					Vector3 normal = hit.normal;	
					//get the angle to the FLoor

					float angle = Library.getAngle(Mathf.Asin(normal.y/normal.magnitude), Mathf.PI * 2, true); 
	//				Debug.Log("Hit: " + hit.point.y + "position: " + futureposition.y + "Subtract: " + (hit.point.y - transform.position.y));

					//find if the scale is too high

					//Debug.Log("angle: " + angle.ToString() + " normal: " + Library.getVector(normal));

					if (angle > (Mathf.PI/2) + scaleAngle || angle < (Mathf.PI/2) - scaleAngle){ //may cause problems from slight deviation of y axis

						//Debug.Log("too High: " + angle.ToString() + " " + scaleAngle.ToString());

	

						return;

					}

					transform.position = hit.point;// + transform.GetComponent.<Collider>().bounds.size.y/2; //move the y axis there

					var x= 0;

				}

			}

			//Debug.Log("Fall");

	

			transform.position = futureposition;

	

		}

		else {

	//		Debug.Log("Air");

			transform.position += direction*Time.deltaTime*airspeed;

		}

		

		

	}

	

	public void Update () {
			Jump scr = transform.GetComponent(typeof(Jump)) as Jump;
			float speed = (scr.stringifyState() == "Stand" ? floorspeed : airspeed); //i like shorthand ifs
			string typeOfSpeed = (scr.stringifyState() == "Stand" ? "floorspeed" : "airspeed"); //i like shorthand ifs

	//		Debug.Log(scr.stringifyState());

			Vector3 direction = Vector3.zero;
	foreach(KeyCode key in left) {
					if (Input.GetKey (key)){

	//					Debug.Log("left is pressed with key " + key);

						direction += thirdPersonCamera.transform.rotation * (Vector3.left); //checks where is left relative to the camera					

						//make sure player doesn't walk through the floor 

	

	

	 //moves it there

						break;

					}

				}

	            foreach(KeyCode key in right) { 
					if (Input.GetKey (key)){

	//					Debug.Log("right is pressed with key " + key);

						direction += thirdPersonCamera.transform.rotation * (Vector3.right); ////checks where is right relative to the camera					

						//make sure player doesn't walk through the floor 

						//direction.y=0;

						//Walk(direction);

						break;

					}

				}

	foreach(KeyCode key in up) {
					if (Input.GetKey (key)){

	//					Debug.Log("up is pressed with key " + key);

						direction += thirdPersonCamera.transform.rotation * (Vector3.forward); ////checks where is up relative to the camera

						//make sure player doesn't walk through the floor 

						//direction.y=0;

						//Walk(direction);

						break;

					}

				}

	foreach(KeyCode key in down) { 
					if (Input.GetKey (key)){

	//					Debug.Log("down is pressed with key " + key);

						direction += thirdPersonCamera.transform.rotation * (-Vector3.forward);

						//make sure player doesn't walk through the floor 

						//Walk(direction);

						break;

					}

				}

				direction.Normalize();	

				Walk(direction);

	

				//UseAbilities();	

				updateQuaternion();

	}

	public void updateQuaternion () {
		Quaternion updated_quaternion = Quaternion.Euler(Quaternion.AngleAxis(currentAngleOfPlayerQuaternion * Mathf.Rad2Deg, currentAxisOfPlayerQuaternion).eulerAngles + new Vector3(-90,0,0));
		if (!Equals(updated_quaternion, transform.rotation))

			transform.rotation = updated_quaternion;

	}

	public void OnCollisionEnter (Collision col) {
		if (col.transform.GetComponent(typeof(FloorEverything)) != null){ //if the player is colliding with a floor;

			Vector3 normal = Library.getAvrgNormal(col);
			float angle = Library.getAngle(Mathf.Asin(normal.y/normal.magnitude), 2*Mathf.PI, false);
            if (angle > Mathf.PI * 0.5 - 1 && angle < Mathf.PI * 0.5 + 1) { //floors

                currentAxisOfPlayerQuaternion = Library.getAvrgNormal(col);


			}

			else { //walls & cellings

	

				Jump scr = transform.GetComponent(typeof(Jump)) as Jump;
				float speed = airspeed ;
				//Library.UncompoundingLog(Time.deltaTime);

				var deltaTime = Time.deltaTime;

				transform.position -= normal * (speed * -15 ) * Time.deltaTime;

			}

	

	

		}

		

	}
}