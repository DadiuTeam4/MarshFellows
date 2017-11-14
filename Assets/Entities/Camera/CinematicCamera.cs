// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace CameraControl
{
    // Scenario enum definition
    public enum Scenario
    {
        Separation,
        Ritual,
        Deer,
        Bear
    }

    public class CinematicCamera : BaseCamera
    {
        public CinematicAnimation[] scenarioAnimations;
        public float damping = 1;

        private Scenario currentScenario;
        private CinematicAnimation currentAnimation;
        private Vector3 scenarioStartPosition;
        private Vector3 scenarioEndPosition;
        public bool isCameraFolloingP = false;

        void Awake()
        {
            // Add event listeners
            EventDelegate eventDelegate = SeparationCallBack;
            EventManager.GetInstance().AddListener(CustomEvent.ScenarioInteracted, SeparationCallBack);
        }

        protected override void UpdatePosition()
        {
            float distanceFromBeginningToHunters = Vector3.Distance(deltaPosition, scenarioStartPosition);
            float distanceFromEndToHunters = Vector3.Distance(deltaPosition, scenarioEndPosition);
            float progress = distanceFromBeginningToHunters / (distanceFromBeginningToHunters + distanceFromEndToHunters);

            Vector3 desiredPosition = currentAnimation.GetPosition(progress, deltaPosition);
            Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
            transform.position = position;

            Vector3 lookAtPos = new Vector3();
            if (isCameraFolloingP)
            {
                lookAtPos = controller.targets[0].position;
            }
            else
            {
                lookAtPos.x = currentAnimation.focusOnHuntersX ? deltaPosition.x : currentAnimation.center.x;
                lookAtPos.y = currentAnimation.focusOnHuntersY ? deltaPosition.y : currentAnimation.center.y;
                lookAtPos.z = currentAnimation.focusOnHuntersZ ? deltaPosition.z : currentAnimation.center.z;
            }

            transform.LookAt(lookAtPos);
        }

        public void SetScenario(Scenario scenario, Vector3 scenarioStartPosition, Vector3 scenarioEndPosition)
        {
            currentScenario = scenario;
            currentAnimation = scenarioAnimations[(int)scenario];

            this.scenarioStartPosition = scenarioStartPosition;
            this.scenarioEndPosition = scenarioEndPosition;
        }

        private void SeparationCallBack(EventArgument argument)
        {
            isCameraFolloingP = true;
        }

    }
}