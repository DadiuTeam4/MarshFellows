// Author: Itai Yavin
// Contributors: Peter Jæger

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Events;
public class Hideable : MonoBehaviour 
{
	public float rayCastDistance = 5.0f;
	[Tooltip("Positions where the hideable can be seen from")]
	public Transform[] checkPositions;
	private bool[] currentlyhidden;

	public LayerMask layerMask;
    public bool drawDebugRays;

    [Tooltip("How often the object will check for whether its hidden. This is measured in how many frames so if 3 it will check every third frame")]
	public int checkFrequency = 1;
	public string falseMessage = "";
	public string trueMessage = "";

	[Header("Extra event call variables")]
	public bool callsExtra = false;
	[Tooltip("Extra event is called only if currentlyhidden bool is same as this")]
	public bool extraEventIfSameAsHiddenBool = false;
	public CustomEvent extraCall;


	private EventArgument argument = new EventArgument();

	void Start()
	{
		argument.gameObjectComponent = gameObject;

		currentlyhidden = new bool[checkPositions.Length];
		Setup();
	}

	void Update () 
	{
		if (Time.frameCount % checkFrequency == 0)
		{
			CheckIfHidden();
		}
	}

	private void Setup()
	{
		RaycastHit hit;
		
		for (int i = 0; i < checkPositions.Length; i++)
		{
			Ray ray = new Ray(transform.position, checkPositions[i].position - transform.position);
			
			if (Physics.Raycast(ray, out hit, rayCastDistance, layerMask))
			{
				currentlyhidden[i] = true;
				continue;
			}

			currentlyhidden[i] = false;
		}
	}

	private void TriggerEvent(bool hidden, int index)
	{
		if(hidden)
		{
			if(!currentlyhidden[index])
			{
				argument.boolComponent = true;
				argument.stringComponent = trueMessage;
				currentlyhidden[index] = true;
				argument.vectorComponent = checkPositions[index].position;
				EventManager.GetInstance().CallEvent(CustomEvent.HiddenByFog, argument);
				
				if(callsExtra && !extraEventIfSameAsHiddenBool)
				{
                    EventManager.GetInstance().CallEvent(extraCall,argument);
				}
			}
		}
		else if(currentlyhidden[index])
		{
			argument.boolComponent = false;
			argument.stringComponent = falseMessage;
			currentlyhidden[index] = false;
			argument.vectorComponent = checkPositions[index].position;
			EventManager.GetInstance().CallEvent(CustomEvent.HiddenByFog, argument);
			
			if(callsExtra && extraEventIfSameAsHiddenBool)
			{
                EventManager.GetInstance().CallEvent(extraCall,argument);
			}
		}
	}

	private void CheckIfHidden()
	{
		RaycastHit hit;
		
		for (int i = 0; i < checkPositions.Length; i++)
		{
			Ray ray = new Ray(transform.position, checkPositions[i].position - transform.position);

			if (Physics.Raycast(ray, out hit, rayCastDistance, layerMask))
			{
                if (drawDebugRays)
                {
                    Debug.DrawLine(transform.position, hit.point, Color.green);
                }
                TriggerEvent(true, i);
				continue;
			}
            if (drawDebugRays)
            {
                Debug.DrawLine(transform.position, checkPositions[i].position, Color.red);
            }
            TriggerEvent(false, i);
		}
	}
}
