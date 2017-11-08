// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Events;
public class TestEventManager : MonoBehaviour 
{	
	void Start () 
	{
		EventManager eventManager = EventManager.GetInstance();

		EventArgument argument = new EventArgument();
		argument.stringComponent = "";

		EventDelegate holdStarted = HoldStarted;
		EventDelegate holdEnded = HoldEnded;
		EventDelegate swipe = Swipe;

		eventManager.AddListener(CustomEvent.HoldBegin, holdStarted);
		eventManager.AddListener(CustomEvent.HoldEnd, holdEnded);
		eventManager.AddListener(CustomEvent.Swipe, swipe);
	}

	public void HoldStarted(EventArgument argument)
	{
		Debug.Log("Hold Begun!");
	}

	public void HoldEnded(EventArgument argument)
	{
		Debug.Log("Hold Ended");
	}

	public void Swipe(EventArgument argument)
	{
		Debug.Log("SWIIIPE!");
	}
}
