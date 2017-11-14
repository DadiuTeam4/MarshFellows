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
		protected Transform pTransform;

		private void Start()
		{
			controller = CameraStateController.GetInstance();
			pTransform = controller.targets[0].name.ToLower() == "p" ? controller.targets[0] : controller.targets[1].name.ToLower() == "p" ? controller.targets[1] : controller.targets[0];
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
