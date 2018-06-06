using UnityEngine;
using System.Collections;
using Noam_Library;

public class Special {

    //private bool isActing = false;


    public delegate void Action(Special zeh);
    public delegate bool Condition(Special zeh);



    Transform character;
	




    private GiveSpecial attachedGiveSpecial;
    public Action act;
    public Condition condition;

    //public Action stop;
    public bool FullAction()
    {
        if (this.condition(this) && attachedGiveSpecial.Stamina > 0 ) {
            this.act(this);
            return true;
        }
        return false;
    }
	public Special (Transform character, Action act, Condition condition ) {
		this.character = character;
        this.act = act;

        this.attachedGiveSpecial = character.GetComponent<GiveSpecial>();
        this.condition = condition;

    }
    

}
