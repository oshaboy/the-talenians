using UnityEngine;
using System.Collections;
using Noam_Library;
public class GiveSpecial : MonoBehaviour {
	

	public enum CharacterClass{Sportman, Lea, Hajime, Naim, Luna, Nathan, Sonya};

	

    private ArrayList all_specials = new ArrayList();
	//public bool isToggle;
	public CharacterClass charClass;




    public float motivationDepletion;
    public float staminaDepletion;
    public float staminaRecoverRate;
    private float stamina;
    private float motivationToStaminaRatio;
    private float motivation; //maxMotivation; //TODO : motivation
    private bool isPressingSpecial;
    private bool didReleaseSpecial;
    private float lastMotivationDrop = 0;
    public KeyCode[] keyCodes;
    public float Stamina
    {
        get
        {
            return stamina;
        }
    }
    public float Motivation
    {
        get
        {
            return motivation;
        }
        set
        {
            motivation = value;
        }
    }
    public void Start () {

        stamina = 1f - Library.delta;
        motivation = 1f - Library.delta;



        switch (charClass){

			case CharacterClass.Luna:

                //special.maxMotivation = 

                Special.Action FloatingJump = delegate(Special zeh){

                    var speed = (transform.GetComponent(typeof(Jump)) as Jump).Speed;
                    transform.position += new Vector3(0, speed * Time.deltaTime, 0);

				    useMentalResources(Time.deltaTime * staminaDepletion);




				};

                //(transform.GetComponent(typeof(Jump))as Jump).abilities.Add(FloatingJump);

                Special.Condition condition_for_floating_jump = delegate (Special zeh)
                {

                    if (isPressingSpecial) {
                         bool[] jumpbools;
                         jumpbools = (transform.GetComponent(typeof(Jump)) as Jump).getJumpVars(); //{ isPressingJump, didReleaseJump, canRejumpqm };
                         var state = (transform.GetComponent(typeof(Jump)) as Jump).CurrentState;

                         //var speed = (transform.GetComponent(typeof(Jump)) as Jump).Speed;



                         if ( state == Jump.State.Fall)
                         {
                             return true;
                         }
                    }
                    
                    return false;
                };

                //special.staminaRecoverRate = staminaRecoverRate;
                all_specials.Add(new Special(transform, FloatingJump, condition_for_floating_jump));

			break;

			case CharacterClass.Sonya:

				Special.Action special = delegate(Special zeh){

					

				};
                Special.Condition c = delegate (Special zeh)
                {
                    return false;
                };

			break;

	
            
	

		}

	}
    public void useMentalResources(float amount)
    {
        stamina -= amount;

        motivation -= amount;

        Debug.Log(stamina);



    }

    public void motivate(float amount)
    {
        motivation += amount;

        stamina = 1f - Library.delta;

    }
    public void Update () {
        bool didDoSomething = false;
        bool prev = isPressingSpecial;
        isPressingSpecial = false;
        foreach (KeyCode button in keyCodes) {
            if (Input.GetKey(button)) {
                isPressingSpecial = true;

            }
        }
        if (prev && !isPressingSpecial)
        {
            didReleaseSpecial = true;
        }
        foreach (Special sp in all_specials)
        {
            didDoSomething |= sp.FullAction();

        }
        if (stamina <= 0 && lastMotivationDrop + 0.5f < Time.time)
        {
            //motivation -= motivationDepletion;
            lastMotivationDrop = Time.time;
        }
        if (didDoSomething && stamina < 1f) {
            stamina += -staminaDepletion * Time.deltaTime;
            //motivation -= motivationDepletion * Time.deltaTime;
        }

        if (!didDoSomething && stamina < 1f) {
            stamina += staminaRecoverRate * Time.deltaTime;
        }
        Debug.Log(stamina);

    }
}