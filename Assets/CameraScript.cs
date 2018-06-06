using UnityEngine;
using System.Collections;
using Noam_Library;
public class CameraScript : MonoBehaviour {
	

	

	public Vector2 initialCameraPosition;
	private Vector2 cameraPosition;
	private Vector2 PrevCamera;
	private Vector2 PrevPosition;
//    private Vector2 PrevRotation;
    private Vector2 lastMousePosition; 
	private Vector2 MouseSpeed;
	public float maxRadius;
	public float minRadius;
	private float curRadius;
	public float speed;
	public Transform character;
	public Library.InputDevice inputDevice;
    public float adjustment_of_floor_camera = 0.3f;
    public float adjustment_of_camera_top_limit = 0.1f;
	private float scrollspeed;
    public float colliderRadius=5;

    private float MouseSlowdown = 1.0f/Library.PixelsPerUnity;

	public float repelStrength = 2;
	public float speedCap;
	//private Vector3 prev;
	

	

	public void Start () {
		(GetComponent(typeof(Camera)) as Camera).RenderWithShader(Shader.Find("MyShader"), "");

		lastMousePosition  = Input.mousePosition;

		curRadius = maxRadius;

		cameraPosition = initialCameraPosition;

		MoveCamera();

	}

	

	public void LateUpdate () {


        //Calculate Mouse Speed by subtracting positions

            //Library.UncompoundingLog("test of compilation");
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
	 		MouseSpeed = mousePosition - lastMousePosition;


			PrevCamera = cameraPosition;

			if (inputDevice == Library.InputDevice.Keyboard) {

				//get the right thing

				if (Input.GetKey (KeyCode.DownArrow)){

					cameraPosition+=(new Vector2(0,1))*Time.deltaTime*speed;

				}

				if (Input.GetKey (KeyCode.UpArrow)){

	

					cameraPosition+=new Vector2(0,-1)*Time.deltaTime*speed;

				}

				if (Input.GetKey (KeyCode.LeftArrow)){

	

					cameraPosition+=new Vector2(-1,0)*Time.deltaTime*speed;

				}

				if (Input.GetKey (KeyCode.RightArrow)){

	

					cameraPosition+=new Vector2(1,0)*Time.deltaTime*speed;

				}

	

			}

			//if the user is using the mouse

			else if (inputDevice == Library.InputDevice.Mouse){

				cameraPosition += MouseSpeed * speed / Library.PixelsPerUnity;

			}

	

			MoveCamera();
        
			//update the mouse position

			lastMousePosition = Input.mousePosition;

	}

	

	public void MoveCamera () {

        float cameraAngle;

        getRadius();
        //Library.UncompoundingLog("cameraPosition.y: " + Library.getAngle(cameraPosition.y, Library.tau, false));

		Quaternion direction =  Quaternion.Euler(cameraPosition.y * Mathf.Rad2Deg, cameraPosition.x * Mathf.Rad2Deg, 0) ;
	

		Vector3 distance = direction * Vector3.forward * curRadius;

        //make sure it doesn't glitch on the floor
        
        if (distance.y < colliderRadius) {
            distance *= (1+(distance.y + colliderRadius) / curRadius);
            distance.y = colliderRadius;
            PrevPosition = transform.position;
            transform.position = character.position + distance;
            transform.LookAt(character);
            Vector3 axis;
            float angle;
            transform.rotation.ToAngleAxis(out angle,out axis);
            axis.y *= -1;
            transform.rotation = Quaternion.AngleAxis(-angle, axis );
            cameraAngle = Library.getAngle(cameraPosition.y, Library.tau, false);
            if (cameraAngle > (0.5 * Mathf.PI)- adjustment_of_floor_camera && cameraAngle < (0.5 * Mathf.PI) + adjustment_of_floor_camera)
            {

                transform.position = PrevPosition;

                cameraPosition = PrevCamera;

               // Library.UncompoundingLog("position 2 : " + transform.position);


            
            }
            return;

        }
		//prev = transform.position;

		PrevPosition = transform.position;

		transform.position = character.position + distance;

		//Library.UncompoundingLog("CameraPosition 1 : " + Library.getVector(cameraPosition) + " radius: " + curRadius);

        /*RaycastHit[] hitarray = Physics.RaycastAll(Ray(transform.position, Vector3.up));
	foreach(RaycastHit hit in hitarray){
			if (hit.transform == transform){

				fixcameraPosition(hit.point, hit.normal);

			}

		}*/
        cameraAngle = Library.getAngle(cameraPosition.y, Library.tau, false);
        if (cameraAngle > (1.5 * Mathf.PI) - adjustment_of_camera_top_limit && cameraAngle < (1.5 * Mathf.PI) + adjustment_of_camera_top_limit) {  

            transform.position = PrevPosition;

			cameraPosition = PrevCamera;

			//Library.UncompoundingLog("position 2 : " + transform.position);

	

		}

		transform.LookAt(character);

	}

	

	public void OnCollisionEnter (Collision collision) { //if the collider is of a floor
		/*if (collision.transform.GetComponent(Terrain) != null){ //if it is a terrain

			transform.position = PrevPosition;

			cameraPosition = PrevCamera;

	

			Library.UncompoundingLog("position 3 : " + transform.position);

		}*/

	

	

	}

	

	public void getRadius () {
		Vector3 direction =  Quaternion.Euler(cameraPosition.y * Mathf.Rad2Deg,  cameraPosition.x * Mathf.Rad2Deg, 0) * Vector3.forward;
		RaycastHit[] allHits= Physics.RaycastAll(character.position, direction);
		if (allHits.Length == 0){

           // Library.UncompoundingLog("changedFrom: " + curRadius);
            curRadius = maxRadius;

		}

		else{

	    foreach(RaycastHit hit in allHits){
				if (hit.transform.GetComponent(typeof(FloorEverything)) != null){ //if it is a floor

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

	

	

}