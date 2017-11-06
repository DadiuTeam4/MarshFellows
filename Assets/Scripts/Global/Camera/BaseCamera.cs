// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraControl
{
	public class BaseCamera : MonoBehaviour 
	{
		public Transform[] targets;
		protected Vector3 deltaDistance;
        protected Vector3 deltaPosition;
		protected bool active = false;

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
			Vector3 deltaDistance = targets[0].position - targets[1].position;
            Vector3 deltaPosition = targets[1].position + (0.5f * deltaDistance);
		}

		public void SetActive(bool value)
		{
			active = value;
		}


		protected virtual void UpdatePosition() {}
	}
}
