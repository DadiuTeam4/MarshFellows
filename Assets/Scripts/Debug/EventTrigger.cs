// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class EventTrigger : MonoBehaviour 
{
	private EventManager eventManager;

	void Update()
	{
		if (Input.GetKeyDown("j"))
		{
			eventManager.CallEvent(CustomEvent.None, new EventArgument());
		}
	}

	void OnEnable()
	{
		EventDelegate eventDelegate = EventCallback;
		eventManager = EventManager.GetInstance();
		eventManager.AddListener(CustomEvent.None, eventDelegate);		
	}

	void EventCallback(EventArgument eventArgument) 
	{
		print("Event triggered: " + eventArgument.eventComponent);
	}
}
