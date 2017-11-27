// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	public class LegacyCinematicCamera : BaseCamera 
	{
		public LegacyCinematicAnimation[] scenarioAnimations;
		public float damping = 1;

		private LegacyCinematicAnimation currentAnimation;
		private Vector3 scenarioStartPosition;
		private Vector3 scenarioEndPosition;

		protected override void UpdatePosition()
		{
			// Position
			float distanceFromBeginningToHunters = Vector3.Distance(deltaPosition, scenarioStartPosition);
			float distanceFromEndToHunters = Vector3.Distance(deltaPosition, scenarioEndPosition);
			float progress = distanceFromBeginningToHunters / (distanceFromBeginningToHunters + distanceFromEndToHunters);

			Vector3 targetPosition = currentAnimation.followP ? pTransform.position : deltaPosition;
			Vector3 desiredPosition = currentAnimation.GetPosition(progress, targetPosition, controller.cameraRig);
			Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
			transform.position = position;

			// Rotation
			Vector3 lookAtPos = new Vector3();
			lookAtPos.x = currentAnimation.focusOnHuntersX ? targetPosition.x : currentAnimation.center.x;
			lookAtPos.y = currentAnimation.focusOnHuntersY ? targetPosition.y : currentAnimation.center.y;
			lookAtPos.z = currentAnimation.focusOnHuntersZ ? targetPosition.z : currentAnimation.center.z;

			transform.LookAt(lookAtPos);

			// Field of view
			float desiredFov = currentAnimation.GetFOV(progress);
			controller.cameraComponent.fieldOfView = Mathf.Lerp(controller.cameraComponent.fieldOfView, desiredFov, Time.deltaTime * damping);

			// Fog
			RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, currentAnimation.fogDensity, Time.deltaTime * damping);
		}

		public void SetScenario(Scenario scenario, Vector3 scenarioStartPosition, Vector3 scenarioEndPosition)
		{
			//currentScenario = scenario;
			currentAnimation = scenarioAnimations[(int) scenario];

			this.scenarioStartPosition = scenarioStartPosition;
			this.scenarioEndPosition = scenarioEndPosition;
		}
	}
}