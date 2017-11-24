// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerFollowingFog : Holdable 
{
	[Tooltip("The minimum speed that the fog will glide at")]
	[Range(0.1f, 0.5f)]	
	public float minimumSpeed = 0.01f;
	[Tooltip("The maximum speed that the fog will glide at")]
	[Range(0.5f, 1.0f)]
	public float maximumSpeed = 0.5f;
	
	[Tooltip("Toggle gliding on release on/off")]
	public bool gliding = true;
	[Tooltip("How often the glide rate is applied, in seconds")]
	[Range(0.01f, 1.0f)]
	public float glideRate = 0.1f;
	[Tooltip("How much speed is lost per tick rate")]
	[Range(0.001f, 1.0f)]
	public float glideDiminishingRate = 0.01f;

	private Vector3 direction;
	private bool newDirectionFound = false;

	public override void OnTouchHold(RaycastHit hit)
	{
		Vector3 newPoint = new Vector3(hit.point.x, transform.position.y, transform.position.z);
		direction = newPoint - transform.position;
		direction = RotateVector(direction, transform.rotation);

		newDirectionFound = true;

		transform.position += direction;
	}

	public override void OnTouchReleased()
	{
		if(gliding)
		{
			StartCoroutine("Glide");
		}
	}

	private IEnumerator Glide()
	{
		float glidingSpeed = CalculateGlideSpeed();

		while(glidingSpeed > 0)
		{
			if (newDirectionFound)
			{
				glidingSpeed = CalculateGlideSpeed();
				newDirectionFound = false;
			}

			transform.position += direction;
			glidingSpeed -= glideDiminishingRate;
			direction = direction.normalized * glidingSpeed;

			yield return new WaitForSeconds(glideRate);
		}
	}

	private float CalculateGlideSpeed()
	{
		float result = direction.magnitude;
		if (result < minimumSpeed)
		{
			result = minimumSpeed;
		}
		else if (result > maximumSpeed)
		{
			result = maximumSpeed;
		}

		return result;
	}

	public Vector3 RotateVector(Vector3 vector, Quaternion rotation)
	{
		Vector3 result;
		Matrix4x4 rotationMatrix = Matrix4x4.Rotate(rotation);
		result = rotationMatrix.MultiplyVector(vector);

		return result;
	}
}
