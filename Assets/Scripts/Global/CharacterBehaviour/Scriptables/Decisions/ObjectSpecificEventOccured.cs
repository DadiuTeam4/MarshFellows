// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace CharacterBehaviour
{
	[CreateAssetMenu(menuName = "Character Behaviour/Decisions/ObjectSpecificEventOccured")]
	public class ObjectSpecificEventOccured : Decision
	{
		public CustomEvent eventName;
		[Range(0.0f, 1.0f)] public float chanceOfReacting = 1.0f;
		
		public bool checkForBool = false;
		public bool checkForThisState = false;

		private bool eventOccured;

		public override bool Decide(StateController controller)
		{
			eventOccured = controller.CheckEventOccured(eventName);

			if (!eventOccured)
			{
				return false;
			}

			controller.SetLatestEventArguments(controller.eventArguments[eventName]);

			if (!controller.latestEventArgument.gameObjectComponent)
			{
				return false;
			}

			if (controller.latestEventArgument.gameObjectComponent != controller.gameObject)
			{
				return false;
			}

			if (checkForBool && controller.latestEventArgument.boolComponent != checkForThisState)
			{
				return false;
			}
			
			float chance = 1 - chanceOfReacting;
			float reactionRoll = Random.Range(0f, 1f);
			return reactionRoll > chance;
		}

	}
}