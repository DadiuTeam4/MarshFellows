// Author: Peter Jæger
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraControl
{
	public class FixedCamera : BaseCamera
	{
		
		public Vector3 fixedPosition;
		private Vector3 CurrentCameraPos;

		void Start () 
		{
			controller = CameraStateController.GetInstance();
		}
		
		protected override void UpdatePosition()
		{
			if (active)
			{
					transform.localPosition = fixedPosition;
			}
		}
	}
}