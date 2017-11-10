// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Events;
public class GroundChecker : MonoBehaviour 
{
	[Tooltip("How often the script checks the ground. This is in seconds. The script will check the ground every X seconds")]
	public float checkFrequency = 1.0f;
	[Tooltip("How far the raycast will be cast.")]
	public float raycastDistance = 5.0f;
	
	public LayerMask layerMask;
	
	private double timeTillNextCheck;
	private EventArgument argument = new EventArgument();
	private string currentGround = "";

	void Start () 
	{
		timeTillNextCheck = checkFrequency;
	}
	
	void Update () 
	{
		if (timeTillNextCheck <= 0.0)
		{
			string groundCheck;
			groundCheck = CheckGround();
			timeTillNextCheck = checkFrequency;

			if (groundCheck != currentGround && groundCheck != "NO GROUND")
			{
				currentGround = groundCheck;
				CallNewGroundEvent();
			}
		}
		else
		{
			timeTillNextCheck -= Time.deltaTime;
		}
	}

	private string CheckGround()
	{
		RaycastHit hit;
		Ray ray = new Ray(transform.position, Vector3.down);

		if (Physics.Raycast(ray, out hit, raycastDistance, layerMask))
		{
			argument.stringComponent = hit.transform.tag;
			argument.gameObjectComponent = gameObject;
			argument.raycastComponent = hit;

			return argument.stringComponent;
		}

		return "NO GROUND";
	}

	public void CallNewGroundEvent()
	{
		EventManager.GetInstance().CallEvent(CustomEvent.GroundChecked, argument);
	}
}
