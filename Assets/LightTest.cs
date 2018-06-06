using UnityEngine;
using System.Collections;
using Noam_Library;
public class LightTest : MonoBehaviour {
	

	public void Start () {
	

	}

	

	public void Update () {
		if (Input.GetKey (KeyCode.J)){

			transform.Rotate(0,0,Time.deltaTime * 90);

		}

		if (Input.GetKey (KeyCode.K)){

			transform.Rotate(0,Time.deltaTime * 90,0);

		}

		if (Input.GetKey (KeyCode.L)){

			transform.Rotate(Time.deltaTime * 90, 0,0);

		}

	}
}