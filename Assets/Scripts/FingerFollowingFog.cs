// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerFollowingFog : Swipeable 
{
	private Vector3 direction;

	public override void OnSwipe(RaycastHit hit, Vector3 direction)
	{
		Vector3 newPoint = new Vector3(hit.point.x, transform.position.y, transform.position.z);
		direction = newPoint - transform.position;

		transform.position += RotateVector(direction, transform.rotation);
	}

	public Vector3 RotateVector(Vector3 vector, Quaternion rotation)
	{
		Vector3 result;

		Matrix4x4 rotationMatrix = Matrix4x4.Rotate(rotation);

		result = rotationMatrix.MultiplyVector(vector);

		return result;
	}
}
