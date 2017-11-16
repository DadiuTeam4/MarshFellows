// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Cinematic Animation")]
public class CinematicAnimation : ScriptableObject 
{
	[Header("Position")]
	public bool followP;
	public AnimationCurve xPosition;
	public bool stickToHuntersX;
	public float xDistanceMultiplier;
	public AnimationCurve xDistance;
	[Space(10)]
	public AnimationCurve yPosition;
	public bool stickToHuntersY;
	public float yDistanceMultiplier;
	public AnimationCurve yDistance;
	[Space(10)]
	public AnimationCurve zPosition;
	public bool stickToHuntersZ;
	public float zDistanceMultiplier;
	public AnimationCurve zDistance;
	[Space(10)]
	[Header("Rotation")]
	public Vector3 center;
	public bool focusOnHuntersX;
	public bool focusOnHuntersY;
	public bool focusOnHuntersZ;
	[Header("Field Of View")]
	public float minFieldOfView = 45;
	public float maxFieldOfView = 20;
	public AnimationCurve fieldOfViewCurve;

	public Vector3 GetPosition(float progress, Vector3 targetPosition, Transform cameraRig)
	{
		float currentDistanceX = xDistance.Evaluate(progress);
		float multipliedDistanceX = xDistanceMultiplier * currentDistanceX;
		float currentDistanceY = yDistance.Evaluate(progress);
		float multipliedDistanceY = yDistanceMultiplier * currentDistanceY;
		float currentDistanceZ = zDistance.Evaluate(progress);
		float multipliedDistanceZ = zDistanceMultiplier * currentDistanceZ;

		Vector3 updatedPosition = targetPosition;
		if (stickToHuntersX)
		{
			updatedPosition.x = xPosition.Evaluate(progress) * multipliedDistanceX;
		}

		if (stickToHuntersY)
		{
			updatedPosition.y = yPosition.Evaluate(progress) * multipliedDistanceY;
		}

		if (stickToHuntersZ)
		{
			updatedPosition.z = zPosition.Evaluate(progress) * multipliedDistanceZ;
		}
		cameraRig.localPosition = updatedPosition;
		
		float x = stickToHuntersX ? cameraRig.position.x + xPosition.Evaluate(progress) * multipliedDistanceX : center.x + xPosition.Evaluate(progress) * multipliedDistanceX;
		float y = stickToHuntersY ? cameraRig.position.y + yPosition.Evaluate(progress) * multipliedDistanceY : center.y + yPosition.Evaluate(progress) * multipliedDistanceY;
		float z = stickToHuntersZ ? cameraRig.position.z + zPosition.Evaluate(progress) * multipliedDistanceZ : center.z + zPosition.Evaluate(progress) * multipliedDistanceZ;  
		
		return new Vector3(x, y, z);
	}

	public float GetFOV(float progress)
	{
		Debug.Log(fieldOfViewCurve.Evaluate(progress));
		return Mathf.Lerp(minFieldOfView, maxFieldOfView, fieldOfViewCurve.Evaluate(progress));
	}
}
