// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraControl;

public class CameraFocusObject : MonoBehaviour 
{
	private CameraStateController mainCamera;

	private void Start()
	{
		mainCamera = CameraStateController.GetInstance();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "O" && mainCamera.GetTrackedObject() == null)
		{
			mainCamera.SetTrackedObject(transform);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "O" && mainCamera.GetTrackedObject() == transform)
		{
			mainCamera.SetTrackedObject(null);
		}
	}
}
