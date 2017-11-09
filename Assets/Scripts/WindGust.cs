// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Events;
[RequireComponent(typeof(WindZone))]
public class WindGust : MonoBehaviour 
{
	private WindZone windZone;
	private ParticleSystem windParticles;

	private EventDelegate eventDelegate;

	void Start () 
	{	
		windZone = GetComponent<WindZone>();

		eventDelegate = PlaceGust;
		EventManager.GetInstance().AddListener(CustomEvent.Swipe, eventDelegate);
	}

	public void PlaceGust(EventArgument argument)
	{
		transform.position = argument.raycastComponent.point;
	}
}
