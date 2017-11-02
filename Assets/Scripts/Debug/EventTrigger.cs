// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class EventTrigger : MonoBehaviour 
{
	private EventManager eventManager;

	void Start()
	{
		eventManager = EventManager.GetInstance();
	}

	void OnEnable()
	{
		
	}

	void Update () 
	{
		
	}
}
