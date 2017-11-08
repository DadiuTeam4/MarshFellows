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
			eventManager.AddListener(CustomEvent.RitualScenarioTriggered, eventDelegate);
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
				case (CustomEvent.SeparationScenarioTriggered):
				{
					currentState = CameraState.Cinematic;
					cinematicCamera.SetScenario(Scenario.Separation, argument.vectorArrayComponent[0], argument.vectorArrayComponent[1]);
					break;
				}
				case (CustomEvent.RitualScenarioTriggered):
				{
					currentState = CameraState.Cinematic;
					cinematicCamera.SetScenario(Scenario.Ritual, argument.vectorArrayComponent[0], argument.vectorArrayComponent[1]);
					break;
				}
				case (CustomEvent.DeerScenarioTriggered):
				{
					currentState = CameraState.Cinematic;
					cinematicCamera.SetScenario(Scenario.Deer, argument.vectorArrayComponent[0], argument.vectorArrayComponent[1]);
					break;
				}
				case (CustomEvent.BearScenarioTriggered):
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