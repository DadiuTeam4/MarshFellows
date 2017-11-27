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
		private Vector3 CurrentCameraPos;

		void Start () 
		{
			controller = CameraStateController.GetInstance();
		}
		
		protected override void UpdatePosition()
		{
			if (active)
			{
				if (fixedCamera)
				{
					transform.localPosition = fixedPosition;
				}
			}
		}
	}
}