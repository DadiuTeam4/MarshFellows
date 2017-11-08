// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraControl
{
	public class BaseCamera : MonoBehaviour 
	{
		protected Vector3 deltaDistance;
        protected Vector3 deltaPosition;
		protected bool active = false;
		protected CameraStateController controller;

		private void Start()
		{
			controller = CameraStateController.GetInstance();
		}

		private void LateUpdate() 
		{
			if (!active)
			{
				return;
			}

			UpdateTargetPosition();
			UpdatePosition();
		}

		private void UpdateTargetPosition()
		{
			deltaDistance = controller.targets[0].position - controller.targets[1].position;
            deltaPosition = controller.targets[1].position + (0.5f * deltaDistance);
		}

		public void SetActive(bool value)
		{
			active = value;
		}


		protected virtual void UpdatePosition() {}
	}
}
