// Author: Peter Jæger
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraControl
{
	public class FixedCamera : BaseCamera
	{
		
		public Transform fixedTransform;
		private Vector3 CurrentCameraPos;

		void Start () 
		{
			controller = CameraStateController.GetInstance();
		}
		
		protected override void UpdatePosition()
		{
			if (active)
			{
				if (fixedTransform)
				{
					transform.position = fixedTransform.position;
				}
				else
				{
					Debug.LogError("Husk nu at sætte startpositionen på cameracontrolleren.");
				}
			}
		}
	}
}