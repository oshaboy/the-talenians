using UnityEngine;
using System.Collections;
using Noam_Library;
public class Normalize_Size : MonoBehaviour {
	public Vector3 scale;
	public static readonly Vector3 fixion = new Vector3(1,2,1);
	

	public void Start () {
		Library.delta = 0.0001f;

		Vector3 bounds = (transform.GetComponent(typeof(Renderer)) as Renderer).bounds.size;
		if (Library.floatEquals(scale.x, scale.magnitude)){ //if scale.y and scale.z are 0

			transform.localScale = (scale.x/bounds.x) * fixion;

		}

		if (Library.floatEquals(scale.y, scale.magnitude)){ //if scale.x and scale.z are 0

			transform.localScale = (scale.y/bounds.y) * fixion;

		}

		if (Library.floatEquals(scale.z, scale.magnitude)){ //if scale.y and scale.x are 0

			transform.localScale = (scale.z/bounds.z) * fixion;

		}

	}

	

}