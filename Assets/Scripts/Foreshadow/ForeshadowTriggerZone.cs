// Author: Jonathan
// Contributers:

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Events;


public class ForeshadowTriggerZone : MonoBehaviour
{
	public GameObject foreshadowEvent;

	public string foreshadowEventToTrigger;

	void OnTriggerEnter(Collider other)
	{
		if (string.Compare(other.transform.name, "O") == 0)
		{
			EventManager eventManager = EventManager.GetInstance();
			EventArgument argument = new EventArgument();

			argument.stringComponent = foreshadowEventToTrigger;
			eventManager.CallEvent(CustomEvent.ForeshadowEventTriggered,
									argument);

			Instantiate(foreshadowEvent, transform.position,
						transform.rotation);
		}
	}
}
