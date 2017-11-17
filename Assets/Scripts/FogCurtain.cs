// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FogCurtain : Swipeable 
{
	[Tooltip("The movement speed of the fog curtain")]
	[Range(0.01f, 1.0f)]
	public float speed = 0.05f;
	
	[Tooltip("The loss of speed of the fog curtain, tick")]
	[Range(0.001f, 1.0f)]
	public float dampening = 0.01f;
	
	[Tooltip("How often the speed is applied in seconds")]
	[Range(0.1f, 5.0f)]
	public float tickRate = 1.0f;

	[Tooltip("How much the animationcurve is scaled on the Z axis of the curtains position")]
	[Range(0.0f, 10f)]
	public float zScalar;

	public AnimationCurve animationCurve;
	
	public Transform positionOneTransform;
	public Transform positionTwoTransform;
	
	private Vector3 firstPosition;
	private Vector3 secondPosition;

	private float lerpT;
	private float currentT;

	private bool coroutineIsRunning = false;
	private IEnumerator glideCoroutine;

	void Start()
	{
		firstPosition = positionOneTransform.position;
		secondPosition = positionTwoTransform.position;

		float distanceFirst = Vector3.Distance(transform.position, firstPosition);
		float distanceFull = Vector3.Distance(firstPosition, secondPosition);
	
		currentT = distanceFirst / distanceFull;
		lerpT = 0.0f;

		glideCoroutine = GlideT(1);
	}

	void Update()
	{
		if (currentT != lerpT)
		{
			lerpT = currentT;
			CalculatePosition();
		}
	}

	private void CalculatePosition()
	{
		Vector3 calculatedPosition;
		calculatedPosition = Vector3.Lerp(firstPosition, secondPosition, lerpT);
		calculatedPosition.z += animationCurve.Evaluate(lerpT) * zScalar;
		transform.position = calculatedPosition;
	}

	public override void OnSwipe(RaycastHit raycastHit, Vector3 direction)
	{
		float dot = Vector3.Dot(Vector3.left, direction);
		
		float directionMagnitude = direction.magnitude;

		if (!coroutineIsRunning)
		{
			if (dot > 0.0f)
			{
				glideCoroutine = GlideT(-1);
			}
			else if (dot < 0.0f)
			{
				glideCoroutine = GlideT(1);
			}

			StartCoroutine(glideCoroutine);
		}
	}

	IEnumerator GlideT(float directionScalar)
	{
		coroutineIsRunning = true;
		float addedSpeed = speed * directionScalar;

		while(addedSpeed != 0.0f)
		{
			if (addedSpeed > 0.0f)
			{
				if (addedSpeed - dampening < 0.0f)
				{
					addedSpeed = 0.0f;
					break;
				}
				else
				{
					addedSpeed -= dampening;
					if (currentT + addedSpeed > 1.0f)
					{
						currentT = 1.0f;
						break;
					}

					currentT += addedSpeed;
				}
			}
			else if (addedSpeed < 0.0f)
			{
				if (addedSpeed + dampening > 0.0f)
				{
					addedSpeed = 0.0f;
					break;
				}
				else
				{
					addedSpeed += dampening;
					if (currentT - addedSpeed < 0.0f)
					{
						currentT = 0.0f;
						break;
					}
		
					currentT += addedSpeed;
				}
			}

			yield return new WaitForSeconds(tickRate);
		}

		coroutineIsRunning = false;
	}
}
