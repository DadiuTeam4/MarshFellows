// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraControl
{
	public class CinematicSpline : MonoBehaviour 
	{
		public SplineController splineController;
		public GameObject splineRoot;

		void Awake()
		{
			splineController.SplineRoot = splineRoot;
		}
	}
}
