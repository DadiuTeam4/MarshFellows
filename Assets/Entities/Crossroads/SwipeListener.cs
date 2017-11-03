// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class SwipeListener : MonoBehaviour 
{
	public GameObject[] blockers;

	private EventManager eventManager;
	private EventDelegate eventDelegate;
	private int openPath = 1;

	void OnEnable()
	{
		eventManager = EventManager.GetInstance();
        eventDelegate += EventCallback;
        eventManager.AddListener(CustomEvent.SwipeEffectStarted, eventDelegate);
        eventManager.AddListener(CustomEvent.SwipeEffectEnded, eventDelegate);
    }


    void Update()
	{
		for (int i = 0; i < blockers.Length; i++)
		{
			blockers[i].SetActive(i != openPath);
		}
	}

	void EventCallback(EventArgument eventArgument)
	{
        print(eventArgument.stringComponent);
		if (eventArgument.eventComponent == CustomEvent.SwipeEffectStarted)
		{
			if (eventArgument.stringComponent == "Left")
			{
				openPath = 0;
			}
			if (eventArgument.stringComponent == "Right")
			{
				openPath = 2;
			}
		}
		else if (eventArgument.eventComponent == CustomEvent.SwipeEffectEnded)
		{
			openPath = 1;
		}
	}
}
