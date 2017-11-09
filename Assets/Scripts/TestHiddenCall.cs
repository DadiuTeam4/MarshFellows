// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Events;
public class TestHiddenCall : MonoBehaviour 
{
	private EventDelegate eventDelegate;

	void Start () 
	{
		eventDelegate = HiddenTest;
		EventManager.GetInstance().AddListener(CustomEvent.HiddenByFog, eventDelegate);
	}

	public void HiddenTest(EventArgument argument)
	{
		Debug.Log(argument.gameObjectComponent.name + " says: " + argument.stringComponent);
	}
}
