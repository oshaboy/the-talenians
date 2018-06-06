#pragma strict

var color : UnityEngine.Color;
// Use this for initialization
function Start () {
	GetComponent.<Renderer>().material.color = color;
}