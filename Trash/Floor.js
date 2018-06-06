#pragma strict


//this is the function attached to all the children. 

var resistance : float;

function Start () {
	//Adds FloorEverything (the collision check function) to all children
	/*var body : Rigidbody = new Rigidbody();
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

/*public static function Collide(){
	isChildColliding = true;
}*/
function Update () {
	
}

function WhatFloorIsColliding(t:Transform){

	//Debug.Log("pass1 " + t);
	for(var i = 0; i < transform.childCount; i++)
	{
		//checks if any of the children are collided using FloorEverything
		var list = new Array();
   		var child : Transform = transform.GetChild(i);
    	var script = child.GetComponent(FloorEverything);
    	if (script.isCollided(t)){
    		//Debug.Log("collided");
    		return child;
    	}
	}
	return null;
}

function lockToFloor(t: Transform, toSet : boolean) : Vector3 {
	if (typeof(toSet) === 'undefined') toSet = true;
	var ray : Ray = new Ray(t.position, Vector3.down);
	var allHits : RaycastHit[] = Physics.RaycastAll(ray);
	var hit : RaycastHit;
	for (var i = 0; i<allHits.length; i++)
		if(Library.getAncestor(allHits[i].transform) == transform)
			hit = allHits[i];
	if (toSet)
		t.position = hit.point;
	return hit.point;
}