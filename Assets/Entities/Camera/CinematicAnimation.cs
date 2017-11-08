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
	public AnimationCurve yPosition;
	public bool trackHuntersY;
	public AnimationCurve zPosition;
	public bool trackHuntersZ;
	public Vector3 center;
	public float distanceMultiplier;
	public AnimationCurve distance;

	public Vector3 GetPosition(float progress, Vector3 targetPosition)
	{
		float currentDistance = (distanceMultiplier * distance.Evaluate(progress));
		float x = trackHuntersX ? targetPosition.x * currentDistance : center.x + xPosition.Evaluate(progress) * currentDistance;
		float y = trackHuntersY ? targetPosition.y * currentDistance : center.y + yPosition.Evaluate(progress) * currentDistance;
		float z = trackHuntersZ ? targetPosition.z * currentDistance : center.z + zPosition.Evaluate(progress) * currentDistance;  
		
		return new Vector3(x, y, z);
	}
}
