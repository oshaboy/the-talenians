#pragma strict

public class Special{
	public var isActing : boolean = false;

	public var motivationToStaminaRatio : float;
	public final var maxStamina : float = 100;
	//public var maxMotivation : float = maxStamina * motivationToStaminaRatio; //TODO : motivation
	public var stamina : float = maxStamina - Library.delta;
	public var motivation : float = 0; //maxMotivation; //TODO : motivation
	public var staminaRecoverRate : float;


	var character : Transform;

	public function useMentalResources(amount : float){
		stamina -= amount;
		motivation -= amount;
		Debug.Log(stamina);
		
	}
	function motivate(amount : float){
		motivation += amount;
		stamina = maxStamina - Library.delta;
	}
	function Update(){
		Debug.Log(stamina);
		/*if (isActing)
			Library.doAbsolutelyNothing();*/
		stamina += (staminaRecoverRate * Time.deltaTime * Library.algebraicbool(!isActing)) * (maxStamina-stamina) / maxStamina;

	}
	public var act : function() = function() {
	};
	public var stop : function() = function() {
	};
	public function Special (c : Transform) {
		character = c;
	}
}