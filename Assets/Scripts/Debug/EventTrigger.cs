// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class EventTrigger : MonoBehaviour 
{

	private EventManager eventManager;


    void OnEnable()
	{
		eventManager = EventManager.GetInstance();
	}

	void Update()
	{
		if (Input.GetKeyDown("j"))
		{
			EventArgument argument = new EventArgument();
			/* argument.vectorArrayComponent = new Vector3[2];
			argument.vectorArrayComponent[0] = startTrigger.position;
			argument.vectorArrayComponent[1] = endTrigger.position; */
			EventManager.GetInstance().CallEvent(CustomEvent.RitualScenarioEntered, argument);
		}
        if (Input.GetKeyDown("s"))
        {
            EventArgument argument = new EventArgument();
            /* argument.vectorArrayComponent = new Vector3[2];
			argument.vectorArrayComponent[0] = startTrigger.position;
			argument.vectorArrayComponent[1] = endTrigger.position; */
            EventManager.GetInstance().CallEvent(CustomEvent.SeparationScenarioEntered, argument);
        }
        if (Input.GetKeyDown("k"))
        {
            EventArgument argument = new EventArgument();
            /* argument.vectorArrayComponent = new Vector3[2];
			argument.vectorArrayComponent[0] = startTrigger.position;
			argument.vectorArrayComponent[1] = endTrigger.position; */
            EventManager.GetInstance().CallEvent(CustomEvent.ScenarioInteracted, argument);
        }

    }
}

