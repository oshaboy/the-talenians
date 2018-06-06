#pragma strict
public class LinkedList{
	public var next : LinkedList;
	public var value;

	function last() : LinkedList{
		if (next == null)
			return this;
		return next.last();
	}
	function length() : int{
		if (next == null){
			return 1;
		}
		return next.length() + 1;
	}
	function getAt(index:int):LinkedList{
		if (index == 0)
			return this;
		if (next == null)
			return null;
		return next.getAt(index-1);
	}

	function add(item){
		next = LinkedList(item, next);
	}

	public function LinkedList(arr : Object[]){
		for(var i = 0; i<arr.length ; i++){
			last().add(arr[i]);
		}

	}

	public function LinkedList(item, next:LinkedList){
		this.next = next;
		value = item; 
	}
}

public class BinaryTree{
	var item;
	var yes : BinaryTree;
	var no : BinaryTree;

	function choose(choice : boolean){
		return choice ? yes : no;
	}
	function BinaryTree (item, yes : BinaryTree, no :BinaryTree){
		this.item = item;
		this.yes = yes;
		this.no = no;
	}
}

public class FlowChart{
	var next : FlowChart;
	var item : Object;
	var no : FlowChart; 
	function get isSplit() : boolean{
		return no!=null;
	}
	function isEnd() : boolean{
		return next == null;
	}
	function getNext(ans : boolean){
		if (isSplit && !ans){
			return no;
		}
		else{
			return next;
		}
	}

	function FlowChart(item, next :FlowChart, no : FlowChart){
		this.item = item;
		this.next = next;
		this.no = no;
		//isSplit = (no != null);
	}
	function FlowChart(item, next :FlowChart){
		FlowChart(item, next, null);
		//isSplit = (no != null);
	}

	function ToString() : String{
		if (isSplit)
			return item.ToString() + "{yes:" + next.ToString() + "no:" + no.ToString() + "}";
		if (isEnd())
			return item.ToString() + "|" + next.ToString();
		return item.ToString() + ";";
	}
	function FlowChart(assetName : String){
		var length : int = (Resources.Load(assetName) as TextAsset).bytes.length;
		FlowChart(assetName, 0, length);
	}
	function FlowChart(assetName : String, startIndex : int, endIndex : int){
		var allchars : byte[] = (Resources.Load(assetName) as TextAsset).bytes;
		var item : String = "";
		var i : int;
		var j : int;
		for (i = startIndex; i<endIndex; i++){
			if (allchars[i] == '0x50'){
				this.item=item;
				if (allchars[i+1] == '0x60'){
					j=i+1;
					while(true){
						if (allchars[j] == '0x50' && allchars[j+1] == '0x70'){
							
							next = FlowChart(assetName, i+2, j);
							no = FlowChart(assetName, j+2, endIndex);
							break;
						}
						j++;
					}
				}
				else{
					//this.item=item;
					next =  FlowChart(assetName, i+1, endIndex);
				}
				return;

			}
			else{
				item += allchars[i];
			}

		}
	} 




}