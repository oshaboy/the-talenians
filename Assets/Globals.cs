using UnityEngine;
using System.Collections;
using Noam_Library;
    public class Globals : MonoBehaviour {


        //that script should be attached to the father of all the tranforms

        public static Hashtable transformTable;
        public void Start() {
            ArrayList allChildren = Library.AllChildren(transform);
            transformTable = new Hashtable();

            foreach (Transform child in allChildren) {
                transformTable.Add(child.name, child);

            }

        }





        static Transform getFromTable(string key) {
            return transformTable[key] as Transform;

        }

        public void Update() {


        }
    }