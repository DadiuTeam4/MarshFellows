// Author: Mathias Dam Hedelund
// Contributors: You Wu, tilemachos
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace CameraControl
{
    public class ThirdPersonCamera : BaseCamera
    {
        [Header("Position")]
        public Vector3 adjustableOffset;
        [Header("Rotation")]
        public float pitch;
        [Header("Damping")]
        public float positionDamping = 1;
        public float rotationDampingY = 1;
        public float rotationDampingX = 1;
        [Header("Tracking")]
        public bool isFollowingCenter = true;
        [Header("Field of View")]
        public float fieldOfView = 38;

        [SerializeField]
        private Transform trackedObject;
        private Vector3 startRotation;

        private float fogDensity;
        private float acceptableFogOffset = 0.01f;
        private GlobalConstants constants;

        private void Start()
        {
            constants = GlobalConstantsManager.GetInstance().constants;
            fogDensity = constants.fogDensity;

            controller = CameraStateController.GetInstance();
            InitTargets();
            FollowPWhenODies();
            startRotation = transform.eulerAngles;
            
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
            }
        }

        private void FollowPWhenODies()
        {
            EventManager.GetInstance().AddListener(CustomEvent.ODead, FollowP);
        }

        private void FollowP(EventArgument argument)
        {
            isFollowingCenter = false;
            Debug.Log("Follow P");
        }

        protected override void UpdatePosition()
        {
            Quaternion rotation = Quaternion.identity;
            if (!trackedObject)
            {
                float currentAngleY = transform.eulerAngles.y;
                float desiredAngleY = pTransform.eulerAngles.y;
                float angleY = Mathf.LerpAngle(currentAngleY, desiredAngleY, Time.deltaTime * rotationDampingY);
                
                float currentAngleX = transform.eulerAngles.x;
                float angleX = Mathf.LerpAngle(currentAngleX, pitch, Time.deltaTime * rotationDampingX);

                rotation = Quaternion.Euler(angleX, angleY, startRotation.z);
                
            }
            else
            {
                Vector3 direction = trackedObject.position - transform.position;
                Quaternion desiredRotation = Quaternion.LookRotation(direction);
                rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * rotationDampingY);
            }
            transform.rotation = rotation;

            Vector3 position = Vector3.Lerp(transform.position, GetDesiredPosition(transform.eulerAngles.y), Time.deltaTime * positionDamping);

            transform.position = position;
            if (controller.cameraComponent.fieldOfView != fieldOfView)
            {
                controller.cameraComponent.fieldOfView = Mathf.Lerp(controller.cameraComponent.fieldOfView, fieldOfView, Time.deltaTime);
            }

            float renderFogDifference = Mathf.Abs(RenderSettings.fogDensity - fogDensity);
            if (renderFogDifference > acceptableFogOffset)
            {
                RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, fogDensity, Time.deltaTime);
            }
        }

        private Vector3 GetDesiredPosition(float yRotation)
        {
            Quaternion currentRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            if (isFollowingCenter)
            {
                return deltaPosition + (currentRotation * adjustableOffset);
            }
            else
            {
                return pTransform.position + (currentRotation * adjustableOffset);
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