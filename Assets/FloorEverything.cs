using UnityEngine;
using System.Collections;
using Noam_Library;
public class FloorEverything : MonoBehaviour {




    //This is the script attached to every single floor tile

    public Transform floor;
    //private ArrayList collidedList;
    private ArrayList collisions = new ArrayList();
    ///private ArrayList collidedSpeeds;




    public Transform Floor {
        get
        {
            return floor;
        }

    }

    public void Start() {
        //collidedList = new ArrayList();

        //collisions = new[] Collision;
        Library.doAbsolutelyNothing();

    }



    ArrayList Collisions {
        get{
		    return collisions;
        }

	}

	public void OnCollisionEnter (Collision collision) {
		if (transform.name == "Cube")

			Debug.Log("Cube");

	//	Debug.Log("collided");

		//collidedList.Push(collision.collider.transform);

		collisions.Add(collision);

		//collision.collider.transform.GetComponent(Jump).togglefinish();

	

	}

	

	

	

	public Collision getCollision (Transform t) {
	foreach(Collision collision in collisions)
			if (collision.collider.transform == t)

				return collision;

				

		return null;

	}

	public static bool collisionCompare (Collision collision1, Collision collision2) {
		return collision1.collider.transform == collision2.collider.transform;

	}

	public void OnCollisionExit (Collision collision) {
		//collidedList.Remove(collision.collider.transform);

		collisions.Remove(getCollision(collision.collider.transform));

	//	Debug.Log("exited");

	}

	

	public void Update () {
	foreach(Collision collision in Collisions){
			//find the normal of the median point and add a force away from there

			var contacts = collision.contacts;

			//Debug.Log("collision");

			var normal = Library.getAvrgNormal(collision);

			//Debug.Log(normal);

			//collision.collider.transform.position -= normal * floor.GetComponent(Floor).resistance * Time.deltaTime;

			//Debug.Log(collision);

	

		}

	}

	public bool isCollided (Transform t){
	

		Library.CompareFunction f = delegate (object x, object y) {

			Collision c = x as Collision;
			return c.collider.transform == y;

		};

		return (Library.Index(Collisions.ToArray(), t, f) != -1);

	}

	

	//get the normal of a collision;

	public ContactPoint[] GetContactPoints (Transform t) {
		return getCollision(t).contacts;

	

	}

	

	

}