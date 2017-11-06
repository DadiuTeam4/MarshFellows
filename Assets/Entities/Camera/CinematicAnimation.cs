// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Cinematic Animation")]
public class CinematicAnimation : ScriptableObject 
{
	public AnimationCurve xPosition;
	public AnimationCurve yPosition;
	public AnimationCurve zPosition;
	public Vector3 center;
	public float magnitude;

	public Vector3 GetPosition(float progress)
	{
		return new Vector3(center.x + xPosition.Evaluate(progress) * magnitude, center.y + yPosition.Evaluate(progress) * magnitude, center.z + zPosition.Evaluate(progress) * magnitude);
	}
}
