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
		Ritual = 0,
		Separation = 1,
		Bear = 2,
		Deer = 3
	}

	[RequireComponent(typeof(SplineController))]
	public class CinematicCamera : BaseCamera 
	{
		public CinematicSpline[] cinematicSplines;
		
		private SplineController currentSplineController;
		private Vector3 scenarioStartPosition;
		private Vector3 scenarioEndPosition;

		private void Awake()
		{
			if (cinematicSplines != null && cinematicSplines.Length > 0)
			{
				currentSplineController = cinematicSplines[0].splineController;
			}
			else 
			{
				Debug.LogError("No Cinematic Splines set in the Cinematic Camera component of the CameraController");
			}
		}

		public void SetScenario(Scenario scenario, Vector3 scenarioStartPosition, Vector3 scenarioEndPosition)
		{
			currentSplineController = cinematicSplines[(int) scenario].splineController;

			this.scenarioStartPosition = scenarioStartPosition;
			this.scenarioEndPosition = scenarioEndPosition;

			currentSplineController.FollowSpline();
		}

		protected override void UpdatePosition()
		{
			// Calculate progress
			float distanceFromBeginningToHunters = Vector3.Distance(deltaPosition, scenarioStartPosition);
			float distanceFromEndToHunters = Vector3.Distance(deltaPosition, scenarioEndPosition);
			float progress = distanceFromBeginningToHunters / (distanceFromBeginningToHunters + distanceFromEndToHunters);

			// Get position and rotation
			Transform controllerTransform = currentSplineController.GetTransform();
			transform.position = controllerTransform.position;
			transform.rotation = controllerTransform.rotation;

			// Field of view
			float desiredFov = currentSplineController.GetFOV(progress);
			controller.cameraComponent.fieldOfView = Mathf.Lerp(controller.cameraComponent.fieldOfView, desiredFov, Time.deltaTime);

			// Fog
			RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, currentSplineController.fogDensity, Time.deltaTime);
		}
	}
}
