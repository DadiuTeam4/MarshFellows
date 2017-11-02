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
	private int openPath;

	void OnEnable()
	{
		eventManager = EventManager.GetInstance();
	}

	void Update()
	{
		for (int i = 0; i < blockers.Length; i++)
		{
			blockers[i].SetActive(i == openPath);
		}
	}

	void EventCallback(EventArgument eventArgument)
	{
		if (eventArgument.eventComponent == CustomEvent.Swipe)
		{
			if (eventArgument.stringComponent == "left")
			{
				openPath = 0;
			}
			if (eventArgument.stringComponent == "right")
			{
				openPath = 2;
			}
		}
		else if (eventArgument.eventComponent == CustomEvent.Swipe)
		{
			openPath = 1;
		}
	}
}
