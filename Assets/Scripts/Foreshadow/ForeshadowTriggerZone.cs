// Author: Jonathan
// Contributers:

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Events;

public enum ForeshadowEvents {Scena1,Scena2,Scena3,Scena4,Crossroad,Missout1,Missout2}

public class ForeshadowTriggerZone : MonoBehaviour
{
	public ForeshadowEvents soundToPlay;

	public GameObject foreshadowObject;
	
	void OnTriggerEnter(Collider other)
	{
		if (string.Compare(other.transform.name, "O") == 0)
		{
			EventManager eventManager = EventManager.GetInstance();
			EventArgument argument = new EventArgument();

			argument.stringComponent = soundToPlay.ToString();
			Debug.Log (argument.stringComponent);
			eventManager.CallEvent(CustomEvent.ForeshadowEventTriggered,
									argument);

			foreshadowObject.SetActive(true);
		}
	}
}
