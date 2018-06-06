#pragma strict
//library of random static generic functions
public class Library
{
	enum Axis {x,y,z};
	enum InputDevice {Mouse, Keyboard};
	static var PixelsPerUnity = 100.0; //find exact number when less tired
	static var delta : float;
	static final var tau : float = 2*Mathf.PI;
	//enum AngleType {Degree=360, Radian=2*Mathf.PI, Gradian = 400, Full = 1);
	public static function diagonal(sides:float[]){
		//var args = Array.prototype.slice.call(arguments, 0);
		var sumOfSquares : float=0;
		for (var side in sides)
			sumOfSquares += side*side;
		return Mathf.Sqrt(sumOfSquares);
	}
	public static function sign(x : float){
		if (x==0) return 0;
		if (x>0) return 1;
		else return -1;
	}

	public static function Log2(exponent : float){
		return Mathf.Log(exponent, 2);
	}
	
	public static function Index(array :Array, x, compare : function(Object, Object) : boolean) {
		for (var i = 0; i<array.length; i++){
			var bool = compare(array[i],x);
			if (bool)
				return i;
		}
		return -1;
	}
	public static function average(array : float[]){
		var sum = 0.0;
		for (var num : float in array){
			sum += num;
		}
		return sum / array.length;
	}
	public static function average(array : Vector3[]){
		var sum = new Vector3(0.0,0.0,0.0);
//		Debug.Log(array);
		for (var vect : Vector3 in array){
			sum += vect;
		}
		return sum / array.length;
	}
	public static function average(array : ContactPoint[]) : Vector3{
		var sum = new Vector3(0.0,0.0,0.0);
//		Debug.Log(array);
		for (var point : ContactPoint in array){
			sum += point.point;
		}
		return sum / array.length;
	}
	
	public static function doAbsolutelyNothing(){
		;
	}

	public static function AllChildren(ancestor : Transform){
		var output = new Array();
		AllChildrenRec(output, ancestor);
		return output;
	}
	private static function AllChildrenRec(output : Array, ancestor : Transform){
		output.Push(ancestor);
		for (var i = 0; i<ancestor.childCount; i++){
			AllChildrenRec(output, ancestor.GetChild(i));
			/*bla bla test debugger
			did the debugger completly break the error recognition*/
			/*you do it for her
			and then you do it again
			you do it for her that is to say
			you do it for him*/
		}

	}
	static function algebraicbool (bool : boolean) : int{
		return bool ? 1 : 0;
	}

	public final var objectSet: Object[] = [null];
	public final var integerSet: Object[]  = [0, null];
	public final var floatSetNaN: Object[] = ["NaN", null];
	public final var floatSet: Object[] = ["NaN",0.0, Mathf.Infinity, Mathf.NegativeInfinity, null];
	public final var VectorSet: Object[] = [null, Vector3.zero];
	public final var whitespaceSet: Object[] = [null, ' ', '\0', '\n', '\t'];

	static function getBool(x, falseSet : Object[]) : boolean{
		for (var i = 0 ; i<falseSet.length; i++){
			if (Equals(falseSet[i], "NaN")){
				if (isNaN(x))
					return false;
			}
			if (Equals(x, falseSet[i]))
				return false;
		}
		return true;
	}
	static function getAvrgNormal(c : Collision) : Vector3{
		var normalArray : Vector3[] = new Vector3[c.contacts.length];
		for(var i = 0; i<c.contacts.length ;i++){
			normalArray[i] = c.contacts[i].normal;
		}

		return Library.average(normalArray);
			
	}
	static function floatEquals(floatx : float, floaty : float) : boolean{
		if (Mathf.Abs(floatx-floaty) < delta)
			return true;
		return false;
	}
	//isNaN that works on objects
	static function isNaN(obj){
		return float.IsNaN(float.Parse(obj));
	}
	static function getAncestor(t : Transform)
	{
		var transform : Transform = t;
		while (transform.parent != null)
			transform = transform.parent;
		return transform;
	}

	static function TrigonometricOpposite (angle : float){
		return Mathf.PI - angle;
	}

	static function TrigonometricOpposite (angle : float, anglesperturn : float){
		return (anglesperturn / 2) - angle;
	}

	static function getAngle (a : float, anglesperturn : float, tohalf : boolean) : float{
		//o = false;
		a = a % anglesperturn;
		if (a<0){
			a+=anglesperturn;
		}

		if (a > anglesperturn / 2 && tohalf)
			a = anglesperturn - a;
		

		return a;
			
	}
	static function map (x : float, in_min : float, in_max : float, out_min :float, out_max:float){
		return  (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
	}

	static function UncompoundingLog(obj : Object){
		Debug.Log(obj.ToString() + "       " + Time.time.ToString());
	}

	//Functions that return strings with more precision than Vector3.ToString()
	static function getVector (vect : Vector3) : String {
		return "(" + vect.x.ToString() + ", " + vect.y.ToString() + ", " + vect.z.ToString() + ")";
	}
	static function getVector (vect : Vector2) : String {
		return "(" + vect.x.ToString() + ", " + vect.y.ToString() + ")";
	}
	/*static function findAngleFromVectorToFloor(vect : Vector3) : float{
		return Mathf.sin(vect.y/vect.magnitude)

	}

		/*private static getCollisionVectors(c : Collision) : Vector3{
		var arr : Vector3[] = [];
		for(var point : ContactPoint in c.contacts)
			
	}


	*/
	/*public static function toBool(x){
		if (x.is(boolean))
			return x as boolean;
		if (x.is(Integer))
			return x != 0;
		if (x.is(Array))
			return x.Length != 0;
		if (x.is(double) || x.is(float))
			return x != NaN;

		return x != null;
	}*/
	/*public static function RemoveFromArray(x, array : Array){
		var index = array.Remove(
		if (index == -1) return false;
		array.slice(index, index);
		return true;
	}*.
	
	/*public class Queue{
		
	}*/

	/*
	DEPRACATED, use Mathf.deg2rad and Mathf.rad2deg instad
	public static final var rad = 180 / Mathf.PI;
	public static final var deg =  Mathf.PI/180;
	*/
}
	