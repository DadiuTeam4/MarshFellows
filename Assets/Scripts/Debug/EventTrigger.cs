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
        if (Input.GetKeyDown("d"))
        {
            EventArgument argument = new EventArgument();

            EventManager.GetInstance().CallEvent(CustomEvent.DeerScenarioEntered, argument);
        }
        if (Input.GetKeyDown("j"))
		{
			EventArgument argument = new EventArgument();

			EventManager.GetInstance().CallEvent(CustomEvent.RitualScenarioEntered, argument);
		}
        if (Input.GetKeyDown("b"))
        {
            EventArgument argument = new EventArgument();
 
            EventManager.GetInstance().CallEvent(CustomEvent.BearScenarioEntered, argument);
        }
        if (Input.GetKeyDown("s"))
        {
            EventArgument argument = new EventArgument();

            EventManager.GetInstance().CallEvent(CustomEvent.SeparationScenarioEntered, argument);
        }
        if (Input.GetKeyDown("i"))
        {
            EventArgument argument = new EventArgument();

            EventManager.GetInstance().CallEvent(CustomEvent.ScenarioInteracted, argument);
        }

    }
}

