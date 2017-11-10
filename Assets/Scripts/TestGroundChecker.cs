// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Events;
public class TestGroundChecker : MonoBehaviour 
{
	private EventDelegate printGroundCheck;

	void Start () 
	{
		printGroundCheck = PrintGroundCheck;
		EventManager.GetInstance().AddListener(CustomEvent.GroundChecked, printGroundCheck);
	}

	public void PrintGroundCheck(EventArgument argument)
	{
		Debug.Log(argument.gameObjectComponent.transform.name + " stepped on " + argument.stringComponent + " at: " + argument.raycastComponent.point);
	}

}
