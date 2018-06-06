using UnityEngine;
using System.Collections;
using Noam_Library;
public class FloorShadow : MonoBehaviour {
	private Ray ray;
	private RaycastHit hit;
	//float size;
	

	public void Update () {
	

		ray = new Ray(transform.parent.position + new Vector3(0,1,0), Vector3.down);
		Physics.Raycast(ray, out hit);
		transform.position = hit.point+new Vector3(0, 1/Library.PixelsPerUnity, 0);

		/*Debug.Log("Ray " + ray.ToString());

		Debug.Log("Hit " + hit.point.ToString());*/

	}

}