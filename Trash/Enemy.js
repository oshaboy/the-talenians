#pragma strict
var Player : Transform;
enum EnemySkill {};
var speed : float;
var isHoming : boolean;
var MinimumDistance : float;
var AttackDistance : float;
function Start () {
	
}

function Update () {
	if(isHoming && (MinimumDistance >= (Player.position - transform.position).magnitude)){
		transform.position = Vector3.Lerp(transform.position, Player.position, speed * Time.deltaTime);
		var toSet = true;
		((Globals.transformTable["Floor"] as Transform).GetComponent("Floor") as Floor).lockToFloor(transform, toSet);
	}
	if ((Player.position - transform.position).magnitude <= AttackDistance){
		//attack n stuff
	}
}
