#pragma strict

enum CharacterClass{Sportman, Lea, Hajime, Naim, Luna, Nathan, Sonya};

private var special : Special;
var SpecialKeys : KeyCode[];
var isToggle : boolean;
var staminaDepletion : float;
var charClass : CharacterClass;
var staminaRecoverRate : float;


function Start(){
	special = new Special(transform);
	switch(charClass){
		case CharacterClass.Luna:
			//special.maxMotivation = 
			var FloatingJump : function() = function(){
				var jumpbools : boolean[] = transform.GetComponent(Jump).getJumpVars();
				var state = transform.GetComponent(Jump).CurrentState;
				var speed = transform.GetComponent(Jump).Speed;

				if (jumpbools[0] && jumpbools[1] && state != State.Stand && special.stamina > 0){
					 transform.position += Vector3(0, speed * Time.deltaTime, 0);
					 special.useMentalResources(Time.deltaTime * staminaDepletion);
					 special.isActing = true;
				}
				else{
					special.isActing = false;
				}
			};
			transform.GetComponent(Jump).abilities.Add(FloatingJump);

			special.staminaRecoverRate = staminaRecoverRate;
		break;
		case CharacterClass.Sonya:
			special.act = function(){
				
			};
		break;


	}
}
function Update (){
	/*if(isToggle){
		for (var key : KeyCode in SpecialKeys)
			if (Input.GetKeyUp(key)){
				(special.isActing ? special.stop : special.act)();
			}
	}
	else{
		for (var key : KeyCode in SpecialKeys)
			if (Input.GetKey(key)){
				
				special.act();
			}
			else if (!Input.GetKey(key)){
				
				special.stop();;
			}
	}*/
	special.Update();

}