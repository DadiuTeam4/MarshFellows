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

	public class CinematicCamera : BaseCamera 
	{
		public CinematicAnimation[] scenarioAnimations;
		public float damping = 1;

		private Scenario currentScenario;
		private CinematicAnimation currentAnimation;
		private Vector3 scenarioStartPosition;
		private Vector3 scenarioEndPosition;

		protected override void UpdatePosition()
		{
			float distanceFromBeginningToHunters = Vector3.Distance(deltaPosition, scenarioStartPosition);
			float distanceFromEndToHunters = Vector3.Distance(deltaPosition, scenarioEndPosition);
			float progress = distanceFromBeginningToHunters / (distanceFromBeginningToHunters + distanceFromEndToHunters);

			Vector3 desiredPosition = currentAnimation.GetPosition(progress, deltaPosition);
			Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
			transform.position = position;
			transform.LookAt(currentAnimation.center);
		}

		public void SetScenario(Scenario scenario, Vector3 scenarioStartPosition, Vector3 scenarioEndPosition)
		{
			currentScenario = scenario;
			currentAnimation = scenarioAnimations[(int) scenario];

			this.scenarioStartPosition = scenarioStartPosition;
			this.scenarioEndPosition = scenarioEndPosition;
		}
	}
}