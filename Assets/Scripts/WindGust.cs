// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Events;
public class WindGust : MonoBehaviour 
{
	private WindZone windZone;
	private ParticleSystem windParticles;

	private EventDelegate eventDelegate;

	void Start () 
	{	
		eventDelegate = PlaceGust;
	}

	public void PlaceGust(EventArgument argument)
	{
		transform.position = argument.raycastComponent.point;
	}
}
