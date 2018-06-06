using UnityEngine;
using System.Collections;
using Noam_Library;
public class Floor : MonoBehaviour {
	

	

	//this is the function attached to all the children. 

	

	public float resistance;
	

	public void Start () {
		//Adds FloorEverything (the collision check function) to all children

		/*Rigidbody body = new Rigidbody();
		body.useGravity = false;	

		body.constraints = RigidbodyConstraints.FreezeAll;*/

		/*for(var i = 0; i < transform.childCount; i++)

		{

			var childObject = transform.GetChild(i).gameObject;

	   		childObject.AddComponent(FloorEverything);

	   		//childObject.AddComponent(Rigidbody);

	   		childObject.GetComponent(FloorEverything).floor = transform;

	   		//childObject.GetComponent(Rigidbody).useGravity = false;

	   		//childObject.GetComponent(Rigidbody).constraints = RigidbodyConstraints.FreezeAll;

	   		//childObject.GetComponent(Rigidbody).isKinematic = true;

	   	}*/

	}

	



	public void Update () {
		

	}

	

	public Transform WhatFloorIsColliding (Transform t) {
	

		//Debug.Log("pass1 " + t);

		for(var i = 0; i < transform.childCount; i++)

		{

			//checks if any of the children are collided using FloorEverything

			var list = new ArrayList();

	   		Transform child = transform.GetChild(i);
	    	FloorEverything script = child.GetComponent(typeof(FloorEverything)) as FloorEverything;

	    	if (script.isCollided(t)){

	    		//Debug.Log("collided");

	    		return child;

	    	}

		}

		return null;

	}

	

	public Vector3 lockToFloor(Transform t, bool toSet = true) {

		Ray ray = new Ray(t.position, Vector3.down);
		RaycastHit[] allHits = Physics.RaycastAll(ray);
		RaycastHit hit = allHits[0];
        bool willset = false;

        for (var i = 0; i < allHits.Length; i++)

            if (Library.getAncestor(allHits[i].transform) == transform)
            {
                willset = true;
                hit = allHits[i];
            }

		if (toSet && willset)

			t.position = hit.point;

		return hit.point;

	}
}