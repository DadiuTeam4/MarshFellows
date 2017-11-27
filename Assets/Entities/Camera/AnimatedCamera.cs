// Author: Peter Jæger
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraControl
{
	public class AnimatedCamera : BaseCamera
	{
		
		public Vector3 fixedPosition;
		public bool fixedCamera;

		private new Animation animation;
		private Vector3 CurrentCameraPos;

		void Start () 
		{
			controller = CameraStateController.GetInstance();
			animation = GetComponentInChildren<Animation>();
		}
		
		void LateUpdate()
		{
			if(active)
			{
				if(!fixedCamera)
				{
					animation.Play();
				}
				if(fixedCamera)
				{
					transform.position = fixedPosition;
				}
			}
		}
	}
}