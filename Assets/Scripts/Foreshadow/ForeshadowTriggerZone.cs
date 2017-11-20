// Author: Jonathan
// Contributers:

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Events;

public enum ForeshadowEvents {Scena1,Scena2,Scena3,Scena4,Crossroad} // Notice: Don't put spaces in enum list, it confuses audiomanager.

public class ForeshadowTriggerZone : MonoBehaviour
{
	public ForeshadowEvents current;

	public GameObject foreshadowObject;
	
	void OnTriggerEnter(Collider other)
	{
		if (string.Compare(other.transform.name, "O") == 0)
		{
			EventManager eventManager = EventManager.GetInstance();
			EventArgument argument = new EventArgument();

			argument.stringComponent = current.ToString();
			Debug.Log (argument.stringComponent);
			eventManager.CallEvent(CustomEvent.ForeshadowEventTriggered,
									argument);

			foreshadowObject.SetActive(true);
			//Instantiate(foreshadowObject, foreshadowObject.transform.position,
			//			transform.rotation);
		}
	}
}
