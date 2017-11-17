// Author: Mathias Dam Hedelund
// Contributors: Emil
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
        EventArgument argument = new EventArgument();
        if (Input.GetKeyDown("d"))
        {
            eventManager.CallEvent(CustomEvent.DeerScenarioEntered, argument);
        }
        if (Input.GetKeyDown("j"))
		{
			eventManager.CallEvent(CustomEvent.RitualScenarioEntered, argument);
		}
        if (Input.GetKeyDown("b"))
        { 
            eventManager.CallEvent(CustomEvent.BearScenarioEntered, argument);
        }
        if (Input.GetKeyDown("s"))
        {
            eventManager.CallEvent(CustomEvent.SeparationScenarioEntered, argument);
        }
        if (Input.GetKeyDown("i"))
        {
            eventManager.CallEvent(CustomEvent.ScenarioInteracted, argument);
        }

    }
}

