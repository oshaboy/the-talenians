using UnityEngine;
using System;
using System.Collections;
namespace Noam_Library {
	//library of random static generic functions

	public class Library

	{

		public enum Axis {x,y,z};

		public enum InputDevice {Mouse, Keyboard};

		public static float PixelsPerUnity = 100.0f; //find exact number when less tired

		public static float delta;
		public static readonly float tau = 2*Mathf.PI;
		//enum AngleType {Degree=360, Radian=2*Mathf.PI, Gradian = 400, Full = 1);

	    public static float diagonal (float[] sides) {
			//var args = ArrayList.prototype.slice.call(arguments, 0);

			float sumOfSquares=0;
			foreach (var side in sides)

				sumOfSquares += side*side;

			return Mathf.Sqrt(sumOfSquares);

		}

	public static int sign (float x) {
			if (x==0) return 0;

			if (x>0) return 1;

			else return -1;

		}

	

	public static float Log2 (float exponent) {
			return Mathf.Log(exponent, 2);

		}


    public delegate bool CompareFunction(object x, object y);

    public static int Index (object[] array, object x, CompareFunction compare)
        {
            

            for (int i = 0; i<array.Length; i++){

				bool boolean = compare(array[i],x);

				if (boolean)

					return i;

			}

			return -1;

		}

	public static float average (float[] array) {
		float sum = 0.0f;

	   foreach(float num in array) {
		    sum += num;

		}

		return sum / array.Length;

		}

	public static Vector3 average (Vector3[] array) {
		var sum = new Vector3(0.0f,0.0f,0.0f);

	//		Debug.Log(array);

	    foreach(Vector3 vect in array) {
			sum += vect;

		}

			return sum / array.Length;

		}

		public static Vector3 average(ContactPoint[] array){
			var sum = new Vector3(0.0f,0.0f,0.0f);

	//		Debug.Log(array);

	        foreach(ContactPoint point in array) {
				sum += point.point;

			}

			return sum / array.Length;

		}

		

	public static void doAbsolutelyNothing () {
			;

		}

	

	public static ArrayList AllChildren (Transform ancestor) {
			var output = new ArrayList();

			AllChildrenRec(ref output, ancestor);

			return output;

		}

    public static void AllChildrenRec(ref ArrayList output,  Transform ancestor)
    {
        output.Add(ancestor);

        for (var i = 0; i < ancestor.childCount; i++) {

        AllChildrenRec(ref output, ancestor.GetChild(i));

        /*bla bla test debugger

        did the debugger completly break the error recognition*/

        /*you do it for her

        and then you do it again

        you do it for her that is to say

        you do it for him*/

    }



}

		public static int algebraicbool (bool boolean){
			return boolean ? 1 : 0;

		}

	

		public readonly object[] objectSet = { null };
		public readonly object[] integerSet  = { 0, null };
		public readonly object[] floatSetNaN = { "NaN", null };
		public readonly object[] floatSet = { "NaN", 0.0, Mathf.Infinity, Mathf.NegativeInfinity, null };
		public readonly object[] VectorSet = { null, Vector3.zero };
		public readonly object[] whitespaceSet = { null, ' ', '\0', '\n', '\t' };
	

		static bool getBool(object x, object[] falseSet){
			for (var i = 0 ; i<falseSet.Length; i++){

				if (Equals(falseSet[i], "NaN")){

					if (Double.IsNaN((double)x))

						return false;

				}

				if (Equals(x, falseSet[i]))

					return false;

			}

			return true;

		}

		public static Vector3 getAvrgNormal(Collision c){
			Vector3[] normalArrayList = new Vector3[c.contacts.Length];
			for(var i = 0; i<c.contacts.Length ;i++){

				normalArrayList[i] = c.contacts[i].normal;

			}

	

			return average(normalArrayList);

				

		}

		public static bool floatEquals(float floatx, float floaty){
			if (Mathf.Abs(floatx-floaty) < delta)

				return true;

			return false;

		}

		//isNaN that works on objects

	    public bool isNaN (object obj) {
            if (obj.GetType() == typeof(Double))
                return Double.IsNaN((double)obj);
            else
                return false;

		}

		public static Transform getAncestor(Transform t)
		{

			Transform transform = t;
			while (transform.parent != null)

				transform = transform.parent;

			return transform;

		}

	

	public float TrigonometricOpposite (float angle) {
			return Mathf.PI - angle;

	}

	

	public float TrigonometricOpposite (float angle, float anglesperturn) {
		return (anglesperturn / 2) - angle;

	}

	

	public static float getAngle(float a, float anglesperturn, bool tohalf) {
			//o = false;

			a = a % anglesperturn;

			if (a<0){

				a+=anglesperturn;

			}

	

			if (a > anglesperturn / 2 && tohalf)

				a = anglesperturn - a;

			

	

			return a;

				

		}

	/*public void map (float out_max) {
			return  (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;

	}*/

	

	public static void UncompoundingLog (object obj) {
			Debug.Log(obj.ToString() + "       " + Time.time.ToString());

		}

	

		//Functions that return strings with more precision than Vector3.ToString()

		public static string getVector (Vector3 vect) {
			return "(" + vect.x.ToString() + ", " + vect.y.ToString() + ", " + vect.z.ToString() + ")";

		}

		public static string getVector (Vector2 vect) {
			return "(" + vect.x.ToString() + ", " + vect.y.ToString() + ")";

		}

		/*static function findAngleFromVectorToFloor(Vector3 vect) : float{
			return Mathf.sin(vect.y/vect.magnitude)

	

		}

	

			/*private static getCollisionVectors(Collision c) : Vector3{
			Vector3[] arr = [];
			for(ContactPoint point in c.contacts)
				

		}

	

	

		*/

	/*public void toBool (x) {
			if (x.is(bool))

				return x as bool;

			if (x.is(Integer))

				return x != 0;

			if (x.is(ArrayList))

				return x.Length != 0;

			if (x.is(double) || x.is(float))

				return x != NaN;

	

			return x != null;

		}*/

	/*public void RemoveFromArrayList (ArrayList array) {
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

		
}