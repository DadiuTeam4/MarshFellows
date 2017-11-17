// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Behaviour/Actions/Wait")]
public class Wait : Action 
{

    public override void Act(StateController controller)
    {
		StopMovement(controller);
    }

	private void StopMovement(StateController controller)
	{
		controller.navigator.StopMovement();
	}

}