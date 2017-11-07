// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Events;
public class ObjectPusher : MonoBehaviour 
{
	[Tooltip("How large an offset the raycast will be casted from in y")]
	public float yOffset = 0.5f;
	[Tooltip("A scalar that adjusts the force of the push")]
	public float forceScalar = 1.0f;
	[Tooltip("A scalar that adjusts the length of the push")]
	public float rayDistanceScalar = 1.0f;

	private EventDelegate pushObjects;

	void Start () 
	{		
		pushObjects = PushObjects;
		EventManager.GetInstance().AddListener(CustomEvent.Swipe, pushObjects);
	}
	
	public void PushObjects(EventArgument argument)
	{
		Vector3 rayBegin = argument.raycastComponent.point;
		Vector3 rayEnd = rayBegin + (argument.vectorComponent / Time.deltaTime);
		rayBegin.y += yOffset;
		rayEnd.y += yOffset;
		Ray ray = new Ray(rayBegin, rayEnd);

		//Debug.DrawLine(rayBegin, rayEnd, Color.green, 2.0f);

		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, rayDistanceScalar * (argument.vectorComponent.magnitude / Time.deltaTime)))
		{
			PushableObject pushable = hit.collider.gameObject.GetComponent<PushableObject>();
			
			if(pushable != null)
			{
				float force = (argument.vectorComponent.magnitude / Time.deltaTime) * forceScalar;
				pushable.Push(argument.vectorComponent, force);
			}
		}
	}
}
