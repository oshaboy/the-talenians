using UnityEngine;
using System.Collections;
using Noam_Library;
public class Enemy : MonoBehaviour {
	Transform Player;
	enum EnemySkill {};

	float speed;
	bool isHoming;
	float MinimumDistance;
	float AttackDistance;
	public void Start () {
		

	}

	

	public void Update () {
		if(isHoming && (MinimumDistance >= (Player.position - transform.position).magnitude)){

			transform.position = Vector3.Lerp(transform.position, Player.position, speed * Time.deltaTime);

			var toSet = true;

			((Globals.transformTable["Floor"] as Transform).GetComponent("Floor") as Floor).lockToFloor(transform, toSet);

		}

		if ((Player.position - transform.position).magnitude <= AttackDistance){

			//attack n stuff

		}

	}

}