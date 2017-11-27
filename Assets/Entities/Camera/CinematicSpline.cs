// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraControl
{
	public enum eOrientationMode { NODE = 0, TANGENT }
	[RequireComponent(typeof(SplineInterpolator))]
	public class CinematicSpline : MonoBehaviour 
	{
		[Header("Position")]
		public AnimationCurve progressionCurve;
		
		[Header("Field Of View")]
		public float minFieldOfView = 45;
		public float maxFieldOfView = 20;
		public AnimationCurve fieldOfViewCurve;

		[Header("Fog")]
		public float fogDensity = 0.07f;

		[Header("Spline")]
		public float duration = 10;
		public eOrientationMode orientationMode = eOrientationMode.NODE;
		public eWrapMode wrapMode = eWrapMode.ONCE;
		public bool AutoClose = true;

		public float GetFOV(float progress)
		{
			return Mathf.Lerp(minFieldOfView, maxFieldOfView, fieldOfViewCurve.Evaluate(progress));
		}

		void OnDrawGizmos()
		{
			Transform[] trans = GetTransforms();
			if (trans == null || trans.Length < 2)
				return;

			SplineInterpolator interp = GetComponent(typeof(SplineInterpolator)) as SplineInterpolator;
			SetupSplineInterpolator(interp, trans);
			interp.StartInterpolation(null, false, wrapMode);


			Vector3 prevPos = trans[0].position;
			for (int c = 1; c <= 100; c++)
			{
				float currTime = c * Duration / 100;
				Vector3 currPos = interp.GetHermiteAtTime(currTime);
				float mag = (currPos-prevPos).magnitude * 2;
				Gizmos.color = new Color(mag, 0, 0, 1);
				Gizmos.DrawLine(prevPos, currPos);
				prevPos = currPos;
			}
		}

		void SetupSplineInterpolator(SplineInterpolator interp, Transform[] trans)
		{
			interp.Reset();

			float step = (AutoClose) ? Duration / trans.Length :
				Duration / (trans.Length - 1);

			int c;
			for (c = 0; c < trans.Length; c++)
			{
				if (OrientationMode == eOrientationMode.NODE)
				{
					interp.AddPoint(trans[c].position, trans[c].rotation, step * c, new Vector2(0, 1));
				}
				else if (OrientationMode == eOrientationMode.TANGENT)
				{
					Quaternion rot;
					if (c != trans.Length - 1)
						rot = Quaternion.LookRotation(trans[c + 1].position - trans[c].position, trans[c].up);
					else if (AutoClose)
						rot = Quaternion.LookRotation(trans[0].position - trans[c].position, trans[c].up);
					else
						rot = trans[c].rotation;

					interp.AddPoint(trans[c].position, rot, step * c, new Vector2(0, 1));
				}
			}

			if (AutoClose)
				interp.SetAutoCloseMode(step * c);
		}

		private Transform[] GetTransforms()
		{
			List<Component> components = new List<Component>(GetComponentsInChildren(typeof(Transform)));
			List<Transform> transforms = components.ConvertAll(c => (Transform)c);

			transforms.Remove(transform);
			transforms.Sort(delegate(Transform a, Transform b)
			{
				return a.name.CompareTo(b.name);
			});

			return transforms.ToArray();

			return null;
		}
	}
}
