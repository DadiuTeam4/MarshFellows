// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraControl
{
    public class ThirdPersonCamera : BaseCamera
    {
        public Vector3 ajustablePos;
        public float positionDamping = 1;
        public float rotationDamping = 1;
        private Vector3 offset;

        private void Start()
        {
            controller = CameraStateController.GetInstance();
            offset = transform.position - controller.targets[1].position + (0.5f * (controller.targets[0].position - controller.targets[1].position));
        }

        protected override void UpdatePosition()
        {
            float currentAngle = transform.eulerAngles.y;
            float desiredAngle = controller.targets[0].eulerAngles.y;
            float angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.deltaTime * rotationDamping);
            Quaternion rotation = Quaternion.Euler(transform.eulerAngles.x, angle, 0);
            transform.rotation = rotation;

            Vector3 desiredPosition = controller.targets[0].position - (rotation * Vector3.forward * offset.magnitude) + ajustablePos;
            Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * positionDamping);

            transform.position = position;
        }
    }
}