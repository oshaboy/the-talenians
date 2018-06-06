 #pragma strict
/*
TODO:
	Make jumps ineffictive on steep slopes
*/
public enum State {Jump, Fall, Stand};
var jumpButtons : KeyCode[];
var initialspeed : float;
private var speed : float;
var maxHeight : float;
var minHeight : float;
var floor : Transform;
var refreshCanReJumpQMWhenAngleIs :float;
private var heightJumped : float;
private var state : State;
private var isPressingJump : boolean  = false;
private var didReleaseJump : boolean = true;
private var canRejumpqm : boolean = true;

private var currentFloor : Transform;
private var currentNormal : Vector3;
//var : transform;
private var isOnSlopeOrDidJumpFromOne : boolean;

public var abilities : Array = new Array();;

public function getJumpVars() {
	return [isPressingJump, didReleaseJump, canRejumpqm];
}

function get CurrentState(){
	return state;
}

function get Speed(){
	return speed;
}
function get SlopeNormal(){
	return currentNormal;
}
function Abilities(){
	for (var i = 0; i<abilities.length; i++)
		(abilities[i] as function())();
}
function getState(){
	return state;
}
function stringifyState(){
	if (state == State.Jump)
		return "Jump";
	if (state == State.Fall)
		return "Fall";
	if (state == State.Stand)
		return "Stand";
	Debug.LogError("Noam update Stringify State");
	return "Noam update Stringify State";
}
function Start () {
	speed = initialspeed;
	state = State.Fall;
	isPressingJump = false;
	didReleaseJump = !isPressingJump;
	currentNormal = Vector3.down;
}

/*function togglefinish(){
	didfinish = !didfinish;
}*/
function Update () {

	//check if pressing a jump button
	//didReleaseJump = !isPressingJump || didReleaseJump;
	isPressingJump = false;
	for (var jumpButton : KeyCode in jumpButtons){
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
	currentFloor = floor.GetComponent(Floor).WhatFloorIsColliding(transform);
	if (currentFloor != null){


		isOnSlopeOrDidJumpFromOne = false;

		//var theCollidingFloor= floor.GetComponent(Floor).WhatFloorIsColliding(transform);
		var theCollision : Collision = currentFloor.GetComponent(FloorEverything).getCollision(transform);
		//var collision = currentFloor.GetComponent(FloorEverything).getCollision(transform);



		var averageNormal : Vector3 = Library.getAvrgNormal(theCollision);
		var AngleToFloor : float = Library.getAngle(Mathf.Asin(averageNormal.y/averageNormal.magnitude), 2*Mathf.PI, false);

		var allhits : RaycastHit[];
		currentNormal = -Library.getAvrgNormal(theCollision);
		//Library.UncompoundingLog(-currentNormal);
		//Library.UncompoundingLog("Angle to floor is: " + AngleToFloor.ToString());
		if ( AngleToFloor > (Mathf.PI*1.5-refreshCanReJumpQMWhenAngleIs) && AngleToFloor < (Mathf.PI*1.5+refreshCanReJumpQMWhenAngleIs)){
			//Library.UncompoundingLog(AngleToFloor.ToString() + ", " + refreshCanReJumpQMWhenAngleIs.ToString());
			//reset jumping
			//Debug.Log("hit floor");

			/*make sure is on top the floor, not inside it*/
			var fromTop : Ray = Ray(transform.position + Vector3(0.0,100.0,0.0), Vector3.down);
			allhits = Physics.RaycastAll(fromTop);
			for (var hit : RaycastHit in allhits){
				if (hit.transform == currentFloor && transform.position.y < hit.point.y){
				//	Library.UncompoundingLog("zoom");
					transform.position = hit.point;
					break;
				}
			}
				
			heightJumped = 0.0;
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


					
		}
		else {
			var direction : Vector3 = averageNormal * Time.deltaTime * floor.GetComponent(Floor).resistance;
			direction.y = 0;
			transform.position -= direction;
			isOnSlopeOrDidJumpFromOne = true;

		}



	}
	//Library.UncompoundingLog(heightJumped);
//	Debug.Log("currentNormal: " + Library.getVector(currentNormal));
				//Is currently jumping above max height		||	 //did already start to fall    &&    //did Stop Jumping and reached minheigh  //did jump from an obtuse slope
		if (((((heightJumped >= maxHeight || !isPressingJump) || state == State.Fall) && (heightJumped >= minHeight || heightJumped == 0 )) || currentNormal.y < 0) && currentFloor == null) {
		//move down
		if (currentNormal.y < 0){
			transform.position += speed * Time.deltaTime * currentNormal;
		}
		else{
			transform.position -= Vector3(0, speed * Time.deltaTime, 0);
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
		/*Debug.Log("isPressingJump: " + isPressingJump);
		Debug.Log("heightJumped: " + heightJumped);
		Debug.Log("canRejumpqm: " + canRejumpqm);
		Debug.Log("didReleaseJump: " + didReleaseJump);*/


		//Library.UncompoundingLog("currentNormal = " + Library.getVector(currentNormal));
 		transform.position += speed * Time.deltaTime * currentNormal;

		heightJumped += speed * Time.deltaTime;
		//Debug.Log("jumping");
		otherwise = false;

		state = State.Jump;


//		Debug.Log("up");
		
	}



	Abilities();
	
}

