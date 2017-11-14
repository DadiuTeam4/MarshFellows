﻿// Author: Mathias Dam Hedelund
// Contributors: You Wu
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
        private Transform oTransform;
        public bool isFollowingCenterBetweenPAndO = true;
        private Vector3 offset;

        private void Start()
        {
            controller = CameraStateController.GetInstance();
            InitTargets();
            offset = transform.position - oTransform.position + (0.5f * (pTransform.position - oTransform.position));
        }

        private void InitTargets()
        {
            if (controller.targets[0] == null || controller.targets[1] == null)
            {
                Debug.LogError("At least one of targets in Cam Controller is null!");
            }
            else
            {
                pTransform = controller.targets[0];
                oTransform = controller.targets[1];
            }
        }

        protected override void UpdatePosition()
        {
            float currentAngle = transform.eulerAngles.y;
            float desiredAngle = pTransform.eulerAngles.y;
            float angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.deltaTime * rotationDamping);
            Quaternion rotation = Quaternion.Euler(transform.eulerAngles.x, angle, 0);
            transform.rotation = rotation;

            Vector3 position = Vector3.Lerp(transform.position, getDesiredPosByPOrCenterBetweenOandP(rotation), Time.deltaTime * positionDamping);

            transform.position = position;
        }

        private Vector3 getDesiredPosByPOrCenterBetweenOandP(Quaternion currentRotation)
        {
            if (isFollowingCenterBetweenPAndO)
            {
                return deltaPosition - (currentRotation * Vector3.forward * offset.magnitude) + ajustablePos;
            }
            else
            {
                return pTransform.position - (currentRotation * Vector3.forward * offset.magnitude) + ajustablePos;
            }
        }
    }
}