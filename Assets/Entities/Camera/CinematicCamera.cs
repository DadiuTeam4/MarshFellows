﻿// Author: Mathias Dam Hedelund
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

	[RequireComponent(typeof(Animator))]
	public class CinematicCamera : BaseCamera 
	{
		public Animation[] scenarioAnimations;

		private Scenario currentScenario;
		private Animation currentAnimation;
		private Animator animator;
		private Vector3 scenarioStartPosition;
		private Vector3 scenarioEndPosition;

		private void Start()
		{
			foreach (Animation animation in scenarioAnimations)
			{
				// animation.
			}
		}

		protected override void UpdatePosition()
		{
			float distanceFromBeginningToHunters = Vector3.Distance(deltaPosition, scenarioStartPosition);
			float distanceFromEndToHunters = Vector3.Distance(deltaPosition, scenarioEndPosition);
			float progress = distanceFromBeginningToHunters / (distanceFromBeginningToHunters + distanceFromEndToHunters);

			// transform.position = currentAnimation
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