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

	private Vector3 previousPosition = Vector3.zero;
	private Vector3 previousDirection = Vector3.zero;
	private Vector3 currentDirection = Vector3.zero;

	void Start () 
	{	
		windZone = GetComponent<WindZone>();
		windParticles = GetComponent<ParticleSystem>();

		onSwipe = PlaceGust;
		onSwipeEnd = DisableGust;
		EventManager.GetInstance().AddListener(CustomEvent.Swipe, onSwipe);
		EventManager.GetInstance().AddListener(CustomEvent.SwipeEnded, onSwipeEnd);
	}

	public void DisableGust(EventArgument argument)
	{
		if (!windParticles.isStopped)
		{
			windParticles.Stop();
		}
		windZone.windMain = 0;
	}

	public void PlaceGust(EventArgument argument)
	{
		if (windZone.windMain == 0)
		{
			windZone.windMain = windForce;
		}

		transform.rotation = Quaternion.LookRotation(argument.vectorComponent.normalized);

		transform.position = argument.raycastComponent.point;
		
		if (previousPosition != Vector3.zero && (transform.position - previousPosition) != Vector3.zero)
		{
			previousDirection = currentDirection;
			currentDirection = transform.position - previousPosition;
			currentDirection += previousDirection;
		}

		if (currentDirection.magnitude != 0)
		{
			currentDirection.Normalize();

			transform.rotation = Quaternion.LookRotation(currentDirection);
		}

		if(windParticles.isStopped)
		{
			windParticles.Play();
		}

		previousPosition = transform.transform.position;
	}
}
