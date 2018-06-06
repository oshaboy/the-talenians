#pragma strict


public var initialCameraPosition : Vector2;
private var cameraPosition : Vector2;
private var PrevCamera : Vector2;
private var PrevPosition : Vector3;
private var lastMousePosition : Vector2; 
private var MouseSpeed : Vector2;
var maxRadius:float;
var minRadius:float;
private var curRadius: float;
var speed:float;
var character:Transform;
var inputDevice:Library.InputDevice;
var scrollspeed;
private var MouseSlowdown = 1.0/Library.PixelsPerUnity;
var repelStrength :float = 2;
var speedCap : float;
//private var prev : Vector3;


function Start () {
	GetComponent.<Camera>().RenderWithShader(Shader.Find("MyShader"), "");
	lastMousePosition  = Input.mousePosition;
	curRadius = maxRadius;
	cameraPosition = initialCameraPosition;
	MoveCamera();
}

function LateUpdate () {
		
		//Calculate Mouse Speed by subtracting positions

 		MouseSpeed = Input.mousePosition - lastMousePosition;

		PrevCamera = cameraPosition;
		if (inputDevice == Library.InputDevice.Keyboard) {
			//get the right thing
			if (Input.GetKey (KeyCode.DownArrow)){
				cameraPosition+=Vector2(0,1)*Time.deltaTime*speed;
			}
			if (Input.GetKey (KeyCode.UpArrow)){

				cameraPosition+=Vector2(0,-1)*Time.deltaTime*speed;
			}
			if (Input.GetKey (KeyCode.LeftArrow)){

				cameraPosition+=Vector2(-1,0)*Time.deltaTime*speed;
			}
			if (Input.GetKey (KeyCode.RightArrow)){

				cameraPosition+=Vector2(1,0)*Time.deltaTime*speed;
			}

		}
		//if the user is using the mouse
		else if (inputDevice == Library.InputDevice.Mouse){
			cameraPosition += MouseSpeed * speed / Library.PixelsPerUnity;
		}
		//Debug.Log("MouseSpeed: " + MouseSpeed);
		//radius -= Input.GetAxis("Mouse ScrollWheel") * speed;

		MoveCamera();
		
		//update the mouse position
		lastMousePosition = Input.mousePosition;
}

function MoveCamera(){

	getRadius();
	var direction : Quaternion =  Quaternion.Euler(cameraPosition.y * Mathf.Rad2Deg, cameraPosition.x * Mathf.Rad2Deg, 0) ;

	var distance : Vector3 = direction * Vector3.forward * curRadius;
	//prev = transform.position;
	PrevPosition = transform.position;
	transform.position = character.position + distance;
	Library.UncompoundingLog("position 1 : " + transform.position);
	/*var hitarray : RaycastHit[] = Physics.RaycastAll(Ray(transform.position, Vector3.up));
	for (var hit : RaycastHit in hitarray){
		if (hit.transform == transform){
			fixcameraPosition(hit.point, hit.normal);
		}
	}*/
	if (distance.y > curRadius-0.1){
		transform.position = PrevPosition;
		cameraPosition = PrevCamera;
		Library.UncompoundingLog("position 2 : " + transform.position);

	}
	transform.LookAt(character);
}

function OnCollisionEnter(collision : Collision){ //if the collider is of a floor
	/*if (collision.transform.GetComponent(Terrain) != null){ //if it is a terrain
		transform.position = PrevPosition;
		cameraPosition = PrevCamera;

		Library.UncompoundingLog("position 3 : " + transform.position);
	}*/


}

function getRadius(){
	var direction : Vector3 =  Quaternion.Euler(cameraPosition.y * Mathf.Rad2Deg,  cameraPosition.x * Mathf.Rad2Deg, 0) * Vector3.forward;
	var allHits : RaycastHit[]= Physics.RaycastAll(character.position, direction);
	if (allHits.length == 0){
		curRadius = maxRadius;
	}
	else{
		for (var hit : RaycastHit in allHits){
			if (hit.transform.GetComponent(FloorEverything) != null){ //if it is a floor
				curRadius = (hit.point - character.position).magnitude;
				break;
			}
		}
		if (curRadius > maxRadius)
			curRadius = maxRadius;
		if (curRadius < minRadius)
			curRadius = minRadius;	
	}

	//curRadius = maxRadius;

}


