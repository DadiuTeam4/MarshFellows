﻿// Author: Itai Yavin
// Contributors: Peter Jæger

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Events;
public class Hideable : MonoBehaviour 
{
	//public FogCurtain hidingCurtain;
	[Tooltip("How often the object will check for whether its hidden. This is measured in how many frames so if 3 it will check every third frame")]
	public int checkFrequency = 1;
	public string falseMessage = "";
	public string trueMessage = "";

	[Header("Extra event call variables")]
	public bool callsExtra = false;
	[Tooltip("Extra event is called only if currentlyhidden bool is same as this")]
	public bool extraEventIfSameAsHiddenBool = false;
	public CustomEvent extraCall;

	private bool currentlyhidden;
	//private GameObject curtain;
	private EventArgument argument = new EventArgument();

	void Start()
	{
		//curtain = hidingCurtain.gameObject;
		currentlyhidden = CheckIfHidden();
		argument.gameObjectComponent = gameObject;
		argument.boolComponent = currentlyhidden;
		EventManager.GetInstance().CallEvent(CustomEvent.HiddenByFog, argument);
	}

	void Update () 
	{
		if (Time.frameCount % checkFrequency == 0)
		{
			if(CheckIfHidden())
			{
				if(!currentlyhidden)
				{
					argument.boolComponent = true;
					argument.stringComponent = trueMessage;
					currentlyhidden = true;
					EventManager.GetInstance().CallEvent(CustomEvent.HiddenByFog, argument);
					
					if(callsExtra && !extraEventIfSameAsHiddenBool)
					{
						EventManager.GetInstance().CallEvent(extraCall);
					}
				}
			}
			else if(currentlyhidden)
			{
				argument.boolComponent = false;
				argument.stringComponent = falseMessage;
				currentlyhidden = false;
				EventManager.GetInstance().CallEvent(CustomEvent.HiddenByFog, argument);
				
				if(callsExtra && extraEventIfSameAsHiddenBool)
				{
					EventManager.GetInstance().CallEvent(extraCall);
				}
			}
		}
	}

	private bool CheckIfHidden()
	{
		RaycastHit hit;
		Ray ray = new Ray(transform.position, Camera.main.transform.position - transform.position);
		if (Physics.Raycast(ray, out hit))
		{
			if (hit.collider.gameObject.tag == "FogCurtain")
			{
				return true;
			}
		}

		return false;
	}
}
