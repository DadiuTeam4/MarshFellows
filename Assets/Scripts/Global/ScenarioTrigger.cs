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

    public string triggerTag = "ScenarioTrigger";

    private bool hasBeenTriggered = false;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag(triggerTag) && !hasBeenTriggered)
		{
			EventArgument argument = new EventArgument();
			argument.vectorArrayComponent = new Vector3[2];
            argument.vectorArrayComponent[0] = transform.position;
            if (otherTriggerZone)
            {
                argument.vectorArrayComponent[1] = otherTriggerZone.position;
            }
            else
            {
                Debug.LogError("No second trigger zone set on Scenario Trigger Zone");
            }
			EventManager.GetInstance().CallEvent(scenarioEvent, argument);
            hasBeenTriggered = true;
		}
	}
}
