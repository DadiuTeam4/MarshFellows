// Author: Mathias Dam Hedelund
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterBehaviour
{
	[CreateAssetMenu (menuName = "Character Behaviour/Actions/LookAt")]
	public class LookAt : Action
	{

		public bool lookForward;

		public override void Act(StateController controller)
		{
			ReactToEvent(controller);
		}

		private void ReactToEvent(StateController controller) 
		{

			controller.LookAt(lookForward);
		}
	}
}