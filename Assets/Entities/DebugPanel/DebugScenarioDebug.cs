// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class DebugScenarioDebug : MonoBehaviour 
{
	EventManager eventManager;

	void Start()
	{
		eventManager = EventManager.GetInstance(); 
	}
	
	public void CallScenarioInteracted()
	{
		eventManager.CallEvent(CustomEvent.ScenarioInteracted);
	}
}
