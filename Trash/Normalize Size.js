 #pragma strict
var scale : Vector3;
final var fixion : Vector3 = Vector3(1,2,1);

function Start () {
	Library.delta = 0.0001;
	var bounds : Vector3 = transform.GetComponent(Renderer).bounds.size;
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

