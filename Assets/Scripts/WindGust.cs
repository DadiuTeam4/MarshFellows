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
	//private ParticleSystem windParticles;

	private EventDelegate onSwipe;
	private EventDelegate onSwipeEnd;

	private Quaternion lookDirection;

	void Start () 
	{	
		windZone = GetComponent<WindZone>();
		//windParticles = GetComponent<ParticleSystem>();

		onSwipe = PlaceGust;
		onSwipeEnd = DisableGust;
		EventManager.GetInstance().AddListener(CustomEvent.Swipe, onSwipe);
		EventManager.GetInstance().AddListener(CustomEvent.SwipeEnded, onSwipeEnd);
	}

	public void DisableGust(EventArgument argument)
	{
		/*if (!windParticles.isStopped)
		{
			windParticles.Stop();
		} */
		windZone.windMain = 0;
	}

	public void PlaceGust(EventArgument argument)
	{
		if (windZone.windMain == 0)
		{
			windZone.windMain = windForce;
		}

		/*if(windParticles.isStopped)
		{
			windParticles.Play();
		}*/

		//Debug.DrawLine(transform.position, transform.position + argument.vectorComponent * 10, Color.red, 1.0f);
		transform.rotation = Quaternion.LookRotation(argument.vectorComponent.normalized);

		transform.position = argument.raycastComponent.point;
	}
}
