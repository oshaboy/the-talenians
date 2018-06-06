#pragma strict
	var left : KeyCode[];
	var right : KeyCode[];
	var up : KeyCode[];
	var down : KeyCode[];
	var thirdPersonCamera : Camera;
	var variable : float;
	public var floorspeed : float;
	public var airspeed : float;
	public var scaleHeight : float;
	var scaleAngle : float;
	private var scaleHeightAsVector : Vector3;
	var xadjust:float;
	var yadjust:float;
	private var currentAxisOfPlayerQuaternion : Vector3;
	private var currentAngleOfPlayerQuaternion : float;
public var abilities : Array = new Array();;

function Abilities(){
	for (var i = 0; i<abilities.length; i++)
		(abilities[i] as function())();
}
function Start () {
	scaleHeightAsVector = new Vector3(0,scaleHeight,0);
	thirdPersonCamera.GetComponent(CameraScript).character = transform;
	currentAxisOfPlayerQuaternion = Vector3.up;
	currentAngleOfPlayerQuaternion = 0;
}

function BuffSpeed(amount : float){
	airspeed *=amount;
	floorspeed *= amount;
}
function Walk(direction : Vector3) {
	direction.y = 0;
	direction.Normalize();
	//get the floor the player is colliding with
	var TheFloor : Transform = Globals.transformTable["Floor"] as Transform;
	var floorscript = TheFloor.GetComponent(Floor);
	var currentFloorPlayerIsHitting = floorscript.WhatFloorIsColliding(transform);
	//var SlopeNormal : Vector3 = transform.GetComponent(Jump).SlopeNormal;
	//var EulerSave : Vector3 = transform.eulerAngles;
	//var angle : float;
	if (direction.magnitude != 0){
		if (direction.x > 0){
			//Library.UncompoundingLog("direction.x is positive");
			currentAngleOfPlayerQuaternion = -Mathf.Asin(direction.z/direction.magnitude) - Mathf.PI/2.0;
		}
		else{
			currentAngleOfPlayerQuaternion = Mathf.Asin(direction.z/direction.magnitude) + (Mathf.PI/2.0);
		}
		// Library.UncompoundingLog(currentAngleOfPlayerQuaternion);
	}
	if (currentFloorPlayerIsHitting != null){ //if it is hitting a floor

		/*
		find if the next position can be scaled and move the y position there
		*/
		var futureposition = transform.position + direction*floorspeed*Time.deltaTime; 
		var ray : Ray = new Ray(futureposition, Vector3.up);
		var allhits : RaycastHit[] = Physics.RaycastAll(ray);
		var i;
		for (var hit : RaycastHit in allhits){

		//Debug.Log(transform.GetComponent(Jump).stringifyState());
			if (hit.transform.Equals(currentFloorPlayerIsHitting)){ //if it is the same floor currently stood on
				var normal : Vector3 = hit.normal;	
				//get the angle to the FLoor
				var angle : float = Library.getAngle(Mathf.Asin(normal.y/normal.magnitude), Mathf.PI * 2, true); 
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

function Update () {
		var scr : Jump = transform.GetComponent(Jump);
		var speed = (scr.stringifyState() == "Stand" ? floorspeed : airspeed); //i like shorthand ifs
		var typeOfSpeed = (scr.stringifyState() == "Stand" ? "floorspeed" : "airspeed"); //i like shorthand ifs
//		Debug.Log(scr.stringifyState());
		var direction : Vector3 = Vector3.zero;
			for (var key : KeyCode in left) {
				if (Input.GetKey (key)){
//					Debug.Log("left is pressed with key " + key);
					direction += thirdPersonCamera.transform.rotation * (Vector3.left); //checks where is left relative to the camera					
					//make sure player doesn't walk through the floor 


 //moves it there
					break;
				}
			}
			for (var key : KeyCode in right) { 
				if (Input.GetKey (key)){
//					Debug.Log("right is pressed with key " + key);
					direction += thirdPersonCamera.transform.rotation * (Vector3.right); ////checks where is right relative to the camera					
					//make sure player doesn't walk through the floor 
					//direction.y=0;
					//Walk(direction);
					break;
				}
			}
			for (var key : KeyCode in up) {
				if (Input.GetKey (key)){
//					Debug.Log("up is pressed with key " + key);
					direction += thirdPersonCamera.transform.rotation * (Vector3.forward); ////checks where is up relative to the camera
					//make sure player doesn't walk through the floor 
					//direction.y=0;
					//Walk(direction);
					break;
				}
			}
			for (var key : KeyCode in down) { 
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

			Abilities();	
			updateQuaternion();
}
function updateQuaternion(){
	var updated_quaternion : Quaternion = Quaternion.Euler(Quaternion.AngleAxis(currentAngleOfPlayerQuaternion * Mathf.Rad2Deg, currentAxisOfPlayerQuaternion).eulerAngles + Vector3(-90,0,0));
	if (!Equals(updated_quaternion, transform.rotation))
		transform.rotation = updated_quaternion;
}
function OnCollisionEnter(col : Collision){
	if (col.transform.GetComponent(FloorEverything) != null){ //if the player is colliding with a floor;
		var normal : Vector3 = Library.getAvrgNormal(col);
		var angle : float = Library.getAngle(Mathf.Asin(normal.y/normal.magnitude), 2*Mathf.PI, false);
		if (angle > Mathf.PI * 0.5 - 1 && angle < Mathf.PI * 0.5 + 1){ //floors
			currentAxisOfPlayerQuaternion = Library.getAvrgNormal(col);
		}
		else { //walls & cellings

			var scr : Jump = transform.GetComponent(Jump);
			var speed = (scr.stringifyState() == "Stand" ? floorspeed : airspeed);
			//Library.UncompoundingLog(Time.deltaTime);
			var deltaTime = Time.deltaTime;
			transform.position -= normal * (speed * variable) * Time.deltaTime;
		}


	}
	
}