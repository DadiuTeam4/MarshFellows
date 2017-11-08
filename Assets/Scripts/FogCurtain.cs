// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FogCurtain : Swipeable 
{
	public float speedScalar = 1.0f;
	[Range(0.01f, 1.0f)]
	public float distanceAccuracy = 0.05f;

	public AnimationCurve animationCurve;
	
	public Transform positionOneTransform;
	public Transform positionTwoTransform;
	
	private Vector3 startPosition;
	private Vector3 endPosition;

	private Vector3 currentStartPosition;
	private Vector3 currentEndPosition;

	private float currentDistance;
	private float distanceFromStart;
	private float distancePercentage;

	private Rigidbody ownRigidbody;

	void Start()
	{
		ownRigidbody = GetComponent<Rigidbody>();

		//SetupStartAndEndPositions();
		startPosition = positionTwoTransform.position;
		endPosition = positionOneTransform.position;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			RaycastHit hit = new RaycastHit();
			Vector3 direction = new Vector3(1.0f, 0.0f, 0.0f);
			OnSwipe(hit, direction);
		}

		if (ownRigidbody.velocity.magnitude > 0)
		{
			EvaluateDistances();
			float lerpT = animationCurve.Evaluate(distancePercentage);
			transform.position = new Vector3(transform.position.x, transform.position.y, GetLerpedPositionZ(lerpT));

			/*if (distancePercentage > 1.0f - distanceAccuracy)
			{
				Vector3 temporary = startPosition;
				startPosition = endPosition;
				endPosition = temporary;
			}*/
		}
	}

	public override void OnSwipe(RaycastHit raycastHit, Vector3 direction)
	{
		float dot = Vector3.Dot(Vector3.left, direction);
		
		float directionMagnitude = direction.magnitude;

		Debug.Log(dot);


		if (dot > 0.0f)
		{
			ownRigidbody.AddForce(new Vector3(-1, 0, 0) * speedScalar * directionMagnitude);
		} 
		else if (dot < 0.0f)
		{
			ownRigidbody.AddForce(new Vector3(1, 0, 0) * speedScalar * directionMagnitude);
		}
	}

	private void EvaluateDistances()
	{
		distanceFromStart = Vector3.Distance(startPosition, transform.position);
		currentDistance = Vector3.Distance(transform.position, endPosition) + distanceFromStart;
		distancePercentage = distanceFromStart / currentDistance;
	}

	private float GetLerpedPositionZ(float t)
	{
		float result = 0.0f;
		float startZ = startPosition.z;
		float endZ = endPosition.z;

		result = Mathf.Lerp(startZ, endZ, t);

		return result;
	}

	private void SetupStartAndEndPositions()
	{
		float leftDistance = Vector3.Distance(transform.position, positionOneTransform.position);
		float rightDistance = Vector3.Distance(transform.position, positionTwoTransform.position);

		if (leftDistance < rightDistance)
		{
			startPosition = positionOneTransform.position;
			endPosition = positionTwoTransform.position;
		}
		else
		{
			startPosition = positionTwoTransform.position;
			endPosition = positionOneTransform.position;
		}
	}
}
