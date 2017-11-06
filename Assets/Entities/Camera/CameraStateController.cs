// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraControl
{
	// Dependencies
	[RequireComponent(typeof(Camera))]
	[RequireComponent(typeof(ThirdPersonCamera))]
	[RequireComponent(typeof(CinematicCamera))]
	[RequireComponent(typeof(CameraShake))]
	public class CameraStateController : MonoBehaviour 
	{
		// State definition
		private enum CameraState
		{
			ThirdPerson,
			Cinematic
		}

		// Variables
		[SerializeField]
		private CameraState currentState;
		private ThirdPersonCamera thirdPersonCamera;
		private CinematicCamera cinematicCamera;
		private Camera camera;

		void Start()
		{	
			camera = GetComponent<Camera>();
			thirdPersonCamera = GetComponent<ThirdPersonCamera>();
			cinematicCamera = GetComponent<CinematicCamera>();
		}

		void Update() 
		{
			CheckState();
			UpdateState();
		}

		void CheckState()
		{

		}

		void UpdateState()
		{
			thirdPersonCamera.SetActive(currentState == CameraState.ThirdPerson);
			cinematicCamera.SetActive(currentState == CameraState.Cinematic);
		}
	}
}