#pragma strict

//that script should be attached to the father of all the tranforms
static var transformTable : Hashtable;
function Start () {
	var allChildren : Array = Library.AllChildren(transform); 
	transformTable = new Hashtable();
	for (var child : Transform in allChildren){
		transformTable.Add(child.name, child);
	} 
}


static function getFromTable(key : String) : Transform{
	return transformTable[key] as Transform;
}
function Update () {

}