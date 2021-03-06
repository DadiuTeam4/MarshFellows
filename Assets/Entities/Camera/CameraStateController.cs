﻿// Author: Mathias Dam Hedelund
// Contributors: Peter Jæger
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace CameraControl
{
	// Dependencies
	[RequireComponent(typeof(ThirdPersonCamera))]
	[RequireComponent(typeof(CinematicCamera))]
	[RequireComponent(typeof(FixedCamera))]
	public class CameraStateController : Singleton<CameraStateController> 
	{
		// State definition
		private enum CameraState
		{
			ThirdPerson,
			Cinematic,
			Fixed
		}

		// Variables
		public Transform[] targets;
		[HideInInspector]
		public Camera cameraComponent;
		[SerializeField]
		private CameraState currentState;
		private ThirdPersonCamera thirdPersonCamera;
		private CinematicCamera cinematicCamera;
		private FixedCamera fixedCamera;
		private EventManager eventManager;

		void Awake()
		{	
			// Get references
			thirdPersonCamera = GetComponent<ThirdPersonCamera>();
			cinematicCamera = GetComponent<CinematicCamera>();
			cameraComponent = GetComponentInChildren<Camera>();
			fixedCamera = GetComponent<FixedCamera>();

			// Add event listeners
			EventDelegate eventDelegate = ScenarioTriggerCallback;
			eventManager = EventManager.GetInstance();
			eventManager.AddListener(CustomEvent.RitualScenarioEntered, eventDelegate);
			eventManager.AddListener (CustomEvent.BearScenarioEntered, eventDelegate);
			eventManager.AddListener (CustomEvent.DeerScenarioEntered, eventDelegate);
			eventManager.AddListener (CustomEvent.CameraDeerSpirit, eventDelegate);
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
				case (CustomEvent.CameraDeerSpirit):
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
			fixedCamera.SetActive(currentState == CameraState.Fixed);
		}

		public void SetTrackedObject(Transform obj)
        {
            thirdPersonCamera.SetTrackedObject(obj);
        }

		public Transform GetTrackedObject()
		{
			return thirdPersonCamera.GetTrackedObject();
		}
	}
}