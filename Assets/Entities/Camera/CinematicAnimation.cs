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
	public float maxFieldOfView = 90;
	public AnimationCurve fieldOfViewCurve;

	public Vector3 GetPosition(float progress, Vector3 targetPosition)
	{
		float currentDistanceX = xDistance.Evaluate(progress);
		float multipliedDistanceX = xDistanceMultiplier * currentDistanceX;
		float currentDistanceY = yDistance.Evaluate(progress);
		float multipliedDistanceY = yDistanceMultiplier * currentDistanceY;
		float currentDistanceZ = zDistance.Evaluate(progress);
		float multipliedDistanceZ = zDistanceMultiplier * currentDistanceZ;

		float x = stickToHuntersX ? targetPosition.x + xPosition.Evaluate(progress) * multipliedDistanceX : center.x + xPosition.Evaluate(progress) * multipliedDistanceX;
		float y = stickToHuntersY ? targetPosition.y + yPosition.Evaluate(progress) * multipliedDistanceY : center.y + yPosition.Evaluate(progress) * multipliedDistanceY;
		float z = stickToHuntersZ ? targetPosition.z + zPosition.Evaluate(progress) * multipliedDistanceZ : center.z + zPosition.Evaluate(progress) * multipliedDistanceZ;  
		
		return new Vector3(x, y, z);
	}
}
