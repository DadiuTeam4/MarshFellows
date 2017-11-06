// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraControl
{
	public class CinematicCamera : MonoBehaviour 
	{
		// Event enum definition
		private enum Event
		{
			Separation,
			Ritual,
			Deer,
			Bear
		}

		private bool active = false;
		private 

		void Start() 
		{
			
		}

		void LateUpdate() 
		{
			if (!active)
			{
				return;
			}
		}

		public void SetActive(bool value)
		{
			active = value;
		}
	}
}