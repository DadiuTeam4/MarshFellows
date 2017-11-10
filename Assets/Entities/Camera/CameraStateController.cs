// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace CameraControl
{
	// Dependencies
	[RequireComponent(typeof(ThirdPersonCamera))]
	[RequireComponent(typeof(CinematicCamera))]
	[RequireComponent(typeof(CameraShake))]
	public class CameraStateController : Singleton<CameraStateController> 
	{
		// State definition
		private enum CameraState
		{
			ThirdPerson,
			Cinematic
		}

		// Variables
		public Transform[] targets;
		[SerializeField]
		private CameraState currentState;
		private ThirdPersonCamera thirdPersonCamera;
		private CinematicCamera cinematicCamera;
		private EventManager eventManager;

		void Awake()
		{	
			// Get references
			thirdPersonCamera = GetComponent<ThirdPersonCamera>();
			cinematicCamera = GetComponent<CinematicCamera>();

			// Add event listeners
			EventDelegate eventDelegate = ScenarioTriggerCallback;
			eventManager = EventManager.GetInstance();
			eventManager.AddListener(CustomEvent.RitualScenarioEntered, eventDelegate);
			eventManager.AddListener (CustomEvent.BearScenarioEntered, eventDelegate);
			eventManager.AddListener (CustomEvent.DeerScenarioEntered, eventDelegate);
			eventManager.AddListener (CustomEvent.SeparationScenarioEntered, eventDelegate);
			eventManager.AddListener(CustomEvent.ScenarioEnded, eventDelegate);
		}

		void Update() 
		{
			UpdateState();
		}

		void ScenarioTriggerCallback(EventArgument argument)
		{
			switch (argument.eventComponent)
			{
				case (CustomEvent.SeparationScenarioEntered):
				{
					currentState = CameraState.Cinematic;
					cinematicCamera.SetScenario(Scenario.Separation, argument.vectorArrayComponent[0], argument.vectorArrayComponent[1]);
					break;
				}
				case (CustomEvent.RitualScenarioEntered):
				{
					currentState = CameraState.Cinematic;
					cinematicCamera.SetScenario(Scenario.Ritual, argument.vectorArrayComponent[0], argument.vectorArrayComponent[1]);
					break;
				}
				case (CustomEvent.DeerScenarioEntered):
				{
					currentState = CameraState.Cinematic;
					cinematicCamera.SetScenario(Scenario.Deer, argument.vectorArrayComponent[0], argument.vectorArrayComponent[1]);
					break;
				}
				case (CustomEvent.BearScenarioEntered):
				{
					currentState = CameraState.Cinematic;
					cinematicCamera.SetScenario(Scenario.Bear, argument.vectorArrayComponent[0], argument.vectorArrayComponent[1]);
					break;
				}
				case (CustomEvent.ScenarioEnded):
				{	
					currentState = CameraState.ThirdPerson;
					break;
				}
			}
		}

		void UpdateState()
		{
			thirdPersonCamera.SetActive(currentState == CameraState.ThirdPerson);
			cinematicCamera.SetActive(currentState == CameraState.Cinematic);
		}
	}
}