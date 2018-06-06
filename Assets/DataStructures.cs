using UnityEngine;
using System.Collections;
using Noam_Library;
namespace DataStructures{
	public class LinkedList{

		public LinkedList next;
		public object value;

	

		public LinkedList last(){

			if (next == null)

				return this;

			return next.last();

		}

		public int length(){

			if (next == null){

				return 1;

			}

			return next.length() + 1;

		}

		public LinkedList getAt(int index){
			if (index == 0)

				return this;

			if (next == null)

				return null;

			return next.getAt(index-1);

		}

	

	    public void add (object item) {
			next = new LinkedList(item, next);

		}

	

	public LinkedList (object[] arr) {
			for(var i = 0; i<arr.Length ; i++){

				last().add(arr[i]);

			}

	

		}

	

	public LinkedList (object item, LinkedList next) {
			this.next = next;

			value = item; 

		}

	}

	

	public class BinaryTree{

		object item;

		BinaryTree yes;
		BinaryTree no;
	

	public BinaryTree choose (bool choice) {
			return choice ? yes : no;

		}

	BinaryTree (object item, BinaryTree yes, BinaryTree no) {
			this.item = item;

			this.yes = yes;

			this.no = no;

		}

	}

	

	public class FlowChart{

		public FlowChart next;
		public object item;
		public FlowChart no; 
		bool isSplit {
            get
            {
                return no != null;
            }
		}

		public bool isEnd(){

			return next == null;

		}

	    public FlowChart getNext (bool ans) {
			if (isSplit && !ans){

				return no;

			}

			else{

				return next;

			}

		}



        FlowChart(object item, FlowChart next, FlowChart no) {
			this.item = item;

			this.next = next;

			this.no = no;

			//isSplit = (no != null);

		}

	FlowChart (object item, FlowChart next) : this(item, next, null) {
			

			//isSplit = (no != null);

		}

	

		override public string ToString(){

			if (isSplit)

				return item.ToString() + "{yes:" + next.ToString() + "no:" + no.ToString() + "}";

			if (isEnd())

				return item.ToString() + "|" + next.ToString();

			return item.ToString() + ";";

		}

	    public FlowChart (string assetName) : this(assetName, 0, (Resources.Load(assetName) as TextAsset).bytes.Length) { }
		

	public FlowChart (string assetName, int startIndex, int endIndex) {
			byte[] allchars = (Resources.Load(assetName) as TextAsset).bytes;
			string item = "";
			int i;
			int j;
			for (i = startIndex; i<endIndex; i++){

				if (allchars[i] == 0x50){

					this.item=item;

					if (allchars[i+1] == 0x60){

						j=i+1;

						while(true){

							if (allchars[j] == 0x50 && allchars[j+1] == 0x70){

								

								next = new FlowChart(assetName, i+2, j);

								no = new FlowChart(assetName, j+2, endIndex);

								break;

							}

							j++;

						}

					}

					else{

						//this.item=item;

						next =  new FlowChart(assetName, i+1, endIndex);

					}

					return;

	

				}

				else{

					item += allchars[i];

				}

	

			}

		} 

	

	

	

	

	}
}
