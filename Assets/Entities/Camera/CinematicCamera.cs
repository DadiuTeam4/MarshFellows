// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CameraControl
{
	public class CinematicCamera : BaseCamera 
	{
		public CinematicSpline[] cinematicSplines; 
		
		private CinematicSpline currentSpline;
		private Vector3 scenarioStartPosition;
		private Vector3 scenarioEndPosition;

		public void SetScenario(Scenario scenario, Vector3 scenarioStartPosition, Vector3 scenarioEndPosition)
		{
			//currentScenario = scenario;
			currentSpline = cinematicSplines[(int) scenario];

			this.scenarioStartPosition = scenarioStartPosition;
			this.scenarioEndPosition = scenarioEndPosition;
		}

		protected override void UpdatePosition()
		{
			// Field of view
			// float desiredFov = currentSpline.GetFOV(progress);
			// controller.cameraComponent.fieldOfView = Mathf.Lerp(controller.cameraComponent.fieldOfView, desiredFov, Time.deltaTime);

			// Fog
			RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, currentSpline.fogDensity, Time.deltaTime);
		}
	}
}
