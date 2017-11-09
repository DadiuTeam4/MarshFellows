// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Cinematic Animation")]
public class CinematicAnimation : ScriptableObject 
{
	public AnimationCurve xPosition;
	public bool trackHuntersX;
	public float xDistanceMultiplier;
	public AnimationCurve xDistance;
	[Space(10)]
	public AnimationCurve yPosition;
	public bool trackHuntersY;
	public float yDistanceMultiplier;
	public AnimationCurve yDistance;
	[Space(10)]
	public AnimationCurve zPosition;
	public bool trackHuntersZ;
	public float zDistanceMultiplier;
	public AnimationCurve zDistance;
	[Space(10)]
	public Vector3 center;
	public bool focusOnHuntersX;
	public bool focusOnHuntersY;
	public bool focusOnHuntersZ;

	public Vector3 GetPosition(float progress, Vector3 targetPosition)
	{
		float currentDistanceX = xDistance.Evaluate(progress);
		float multipliedDistanceX = xDistanceMultiplier * currentDistanceX;
		float currentDistanceY = yDistance.Evaluate(progress);
		float multipliedDistanceY = yDistanceMultiplier * currentDistanceY;
		float currentDistanceZ = zDistance.Evaluate(progress);
		float multipliedDistanceZ = zDistanceMultiplier * currentDistanceZ;

		float x = trackHuntersX ? targetPosition.x + multipliedDistanceX : center.x + xPosition.Evaluate(progress) * multipliedDistanceX;
		float y = trackHuntersY ? targetPosition.y + multipliedDistanceY : center.y + yPosition.Evaluate(progress) * multipliedDistanceY;
		float z = trackHuntersZ ? targetPosition.z + multipliedDistanceZ : center.z + zPosition.Evaluate(progress) * multipliedDistanceZ;  
		
		return new Vector3(x, y, z);
	}
}
