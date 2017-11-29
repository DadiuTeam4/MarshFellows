// Contributors: Mathias Dam Hedelund
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CameraControl
{
	public enum eOrientationMode { NODE = 0, TANGENT }

	[AddComponentMenu("Splines/Spline Controller")]
	[RequireComponent(typeof(SplineInterpolator))]
	public class SplineController : MonoBehaviour
	{
		[Header("Spline")]
		public GameObject SplineRoot;
		public float Duration = 10;

		[Header("Field Of View")]
		public float minFieldOfView = 45;
		public float maxFieldOfView = 20;
		public AnimationCurve fieldOfViewCurve;

		[Header("Fog")]
		public float fogDensity = 0.07f;

		private eOrientationMode OrientationMode = eOrientationMode.NODE;
		private eWrapMode WrapMode = eWrapMode.ONCE;
		private bool AutoClose = false;
		private bool HideOnExecute = true;
		private SplineInterpolator mSplineInterp;
		private Transform[] mTransforms;
		private bool drawGizmos = true;

		void OnDrawGizmos()
		{
			if (!drawGizmos)
			{
				return;
			}
			Transform[] trans = GetTransforms();
			if (trans.Length < 2)
				return;

			SplineInterpolator interp = GetComponent<SplineInterpolator>();
			SetupSplineInterpolator(interp, trans);
			interp.StartInterpolation(null, false, WrapMode);


			Vector3 prevPos = trans[0].position;
			for (int c = 1; c <= 100; c++)
			{
				float currTime = c * Duration / 100;
				Vector3 currPos = interp.GetHermiteAtTime(currTime);
				float mag = (currPos-prevPos).magnitude * 2;
				Gizmos.color = new Color(mag, 255 - mag * 100, 0, 1);
				Gizmos.DrawLine(prevPos, currPos);
				prevPos = currPos;
			}
		}

		public float GetFOV(float progress)
		{
			return Mathf.Lerp(minFieldOfView, maxFieldOfView, fieldOfViewCurve.Evaluate(progress));
		}

		// Cinematic Camera class uses this method to set its own position and rotation
		public Transform GetTransform()
		{
			return transform;
		}

		void Start()
		{
			mSplineInterp = GetComponent<SplineInterpolator>();

			mTransforms = GetTransforms();

			if (HideOnExecute)
				drawGizmos = false;
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


		/// <summary>
		/// Returns children transforms, sorted by name.
		/// </summary>
		Transform[] GetTransforms()
		{
			if (SplineRoot != null)
			{
				if (!drawGizmos)
				{
					EnableTransforms();
				}
				List<Transform> transforms = new List<Transform>(SplineRoot.GetComponentsInChildren<Transform>());

				transforms.Remove(SplineRoot.transform);
				transforms.Sort(delegate(Transform a, Transform b)
				{
					return a.name.CompareTo(b.name);
				});
				if (!drawGizmos)
				{
					DisableTransforms();
				}
				return transforms.ToArray();
			}

			return null;
		}

		/// <summary>
		/// Disables the spline objects, we don't need them outside design-time.
		/// </summary>
		void DisableTransforms()
		{
			if (SplineRoot != null)
			{
				SplineRoot.SetActiveRecursively(false);
			}
		}

		void EnableTransforms()
		{
			if (SplineRoot != null)
			{
				SplineRoot.SetActiveRecursively(true);
			}
		}


		/// <summary>
		/// Starts the interpolation
		/// </summary>
		public void FollowSpline()
		{

			mTransforms = GetTransforms();
			mSplineInterp = GetComponent<SplineInterpolator>();
			DisableTransforms();
			if (mTransforms.Length > 0)
			{
				SetupSplineInterpolator(mSplineInterp, mTransforms);
				mSplineInterp.StartInterpolation(null, true, WrapMode);
			}
		}
	}
}