// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Events;
[RequireComponent(typeof(WindZone))]
public class WindGust : MonoBehaviour 
{
	[Tooltip("The amount of force applied from the wind zone component. Negative numbers push, while positive numbers pull")]
	public float windForce = -1.0f;

	private WindZone windZone;
	private ParticleSystem windParticles;

	private EventDelegate onSwipe;
	private EventDelegate onSwipeEnd;

	void Start () 
	{	
		windZone = GetComponent<WindZone>();

		onSwipe = PlaceGust;
		onSwipeEnd = DisableGust;
		EventManager.GetInstance().AddListener(CustomEvent.Swipe, onSwipe);
		EventManager.GetInstance().AddListener(CustomEvent.SwipeEnded, onSwipeEnd);
	}

	public void DisableGust(EventArgument argument)
	{
		Debug.Log("DISABLED");
		windZone.windMain = 0;
	}

	public void PlaceGust(EventArgument argument)
	{
		if (windZone.windMain == 0)
		{
			windZone.windMain = windForce;
		}

		transform.position = argument.raycastComponent.point;
	}
}
