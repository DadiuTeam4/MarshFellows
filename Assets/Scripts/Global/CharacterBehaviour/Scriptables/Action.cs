// Author: Mathias Dam Hedelund
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterBehaviour
{
	public abstract class Action : ScriptableObject 
	{
		public abstract void Act (StateController controller);
	}
}
