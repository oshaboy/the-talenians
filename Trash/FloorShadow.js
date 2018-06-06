#pragma strict
private var ray : Ray;
private var hit : RaycastHit;
//var size : float;

function Update () {

	ray = new Ray(transform.parent.position + Vector3(0,1,0), Vector3.down);
	Physics.Raycast(ray, hit);
	transform.position = hit.point+Vector3(0, 1/Library.PixelsPerUnity, 0);;
	/*Debug.Log("Ray " + ray.ToString());
	Debug.Log("Hit " + hit.point.ToString());*/
}
