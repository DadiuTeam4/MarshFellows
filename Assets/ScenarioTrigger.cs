// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

[RequireComponent(typeof(BoxCollider))]
public class ScenarioTrigger : MonoBehaviour 
{
	public CustomEvent scenarioEvent;
	public Transform otherTriggerZone;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("ScenarioTrigger"))
		{
			EventArgument argument = new EventArgument();
			argument.vectorArrayComponent = new Vector3[2];
			argument.vectorArrayComponent[0] = transform.position;
			argument.vectorArrayComponent[1] = otherTriggerZone.position;
			EventManager.GetInstance().CallEvent(scenarioEvent, argument);
		}
	}
}
