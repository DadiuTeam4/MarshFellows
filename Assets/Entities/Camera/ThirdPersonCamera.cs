// Author: Mathias Dam Hedelund
// Contributors: You Wu
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraControl
{
    public class ThirdPersonCamera : BaseCamera
    {
        private Transform oTransform;
        private Transform trackedObject;
        private Vector3 offset;
        public Vector3 adjustableOffset;
        public float positionDamping = 1;
        public float rotationDamping = 1;
        public bool isFollowingCenter = true;
        public float fieldOfView = 45;

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
            Quaternion rotation = Quaternion.identity;
            if (!trackedObject)
            {
                float currentAngle = transform.eulerAngles.y;
                float desiredAngle = pTransform.eulerAngles.y;
                float angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.deltaTime * rotationDamping);
                rotation = Quaternion.Euler(transform.eulerAngles.x, angle, 0);
            }
            else
            {
                Vector3 direction = trackedObject.position - transform.position;
                Quaternion desiredRotation = Quaternion.LookRotation(direction);
                rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * rotationDamping);
            }
            transform.rotation = rotation;

            Vector3 position = Vector3.Lerp(transform.position, GetDesiredPosition(transform.rotation), Time.deltaTime * positionDamping);

            transform.position = position;
            if (controller.cameraComponent.fieldOfView != fieldOfView)
            {
                controller.cameraComponent.fieldOfView = Mathf.Lerp(controller.cameraComponent.fieldOfView, fieldOfView, Time.deltaTime);
            }
        }

        private Vector3 GetDesiredPosition(Quaternion currentRotation)
        {
            if (isFollowingCenter)
            {
                return deltaPosition - (currentRotation * Vector3.forward * offset.magnitude) + adjustableOffset;
            }
            else
            {
                return pTransform.position - (currentRotation * Vector3.forward * offset.magnitude) + adjustableOffset;
            }
        }

        public void SetTrackedObject(Transform obj)
        {
            trackedObject = obj;
        }

        public Transform GetTrackedObject()
		{
            return trackedObject;
        }
    }
}