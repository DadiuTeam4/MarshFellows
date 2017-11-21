// Author: Peter Jæger
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraControl
{
	public class AnimatedCamera : BaseCamera
	{
		Animation animation;
		public Vector3 fixedPosition;
		public bool fixedCamera;
		private Camera camera;
		// Use this for initialization
		void Start () 
		{
			controller = CameraStateController.GetInstance();
			animation = GetComponentInChildren<Animation>();
			camera = GetComponentInChildren<Camera>();

		}
		
		// Update is called once per frame
		void Update () 
		{
			if(active)
			{
				if(!fixedCamera)
				{
				animation.Play();
				}
				if(fixedCamera)
				{
					camera.transform.position = fixedPosition;
				}

			}
		}


	}
}