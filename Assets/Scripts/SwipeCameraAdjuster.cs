// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeCameraAdjuster : MonoBehaviour 
{	
	private float startX;
	private float startY;

	void Start()
	{
		startX = transform.rotation.eulerAngles.x;
		startY = transform.rotation.eulerAngles.y;
	}

	void Update () 
	{	
		AdjustRotationZ(Camera.main.transform.eulerAngles.y);
	}

	private void AdjustRotationZ(double z)
	{

		Quaternion reset = Quaternion.Euler(0, 0, 0);
		transform.rotation = reset;

		Quaternion target = Quaternion.Euler(startX, startY, (float) -z);
		transform.rotation = target;
	}
}
