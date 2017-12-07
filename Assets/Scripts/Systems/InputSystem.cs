// Author: Mathias Dam Hedelund
// Contributors: Itai Thomas Yavin

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Events;

public class InputSystem : Singleton<InputSystem>
{
	public Camera testingCamera;

	[TextArea(0, 10)]
	public string header = "Handles all touch input. All objects inheriting from the Holdable and Swipable are called accordingly from this class.";
	#region TOUCH_INPUT
	public static List<Vector3> swipeDirections;

	[Tooltip("How far raycasts are casted.")]
	public float raycastDistance = 35;

	private static readonly int maxNumberTouches = 20;
	private Holdable[] heldLastFrame = new Holdable[maxNumberTouches];
	private Holdable[] heldThisFrame = new Holdable[maxNumberTouches];
	private RaycastHit?[] raycastHits = new RaycastHit?[maxNumberTouches];
	private Dictionary<int, List<Vector3>> touchPositions = new Dictionary<int, List<Vector3>>();
	private ParticleSpawner particleSpawner;
    Touch[] touchList = new Touch[maxNumberTouches];

    #endregion

    #region DEBUG
#if UNITY_EDITOR
    // MOUSE DEBUGGING
    // NOT COMPILED IN BUILDS
    bool mouseDownLastFrame;
	#endif
	#endregion

	private void Start()
	{
		particleSpawner = ParticleSpawner.GetInstance();
		// Initialize touch positions
		for (int i = 0; i < maxNumberTouches; i++)
		{
			touchPositions.Add(i, new List<Vector3>());
		}

		// Initialize swipe list
		swipeDirections = new List<Vector3>();
	}

	#region UPDATE_LOOP
	private void Update()
	{
		HandleTouchInput();
	}
	#endregion

	#region TOUCH_INPUT
	private void HandleTouchInput()
	{
		swipeDirections.Clear();
		// Resolve all touches
		Touch[] touches = GetTouches();
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = touches[i];
			switch (touch.phase) 
			{
				case (TouchPhase.Began):
				{
					TouchBegan(touch);
					break;
				}

				case (TouchPhase.Stationary):
				{
					TouchStationary(touch);
					break;
				}

				case (TouchPhase.Moved):
				{
					TouchMoved(touch);
					break;
				}

				case (TouchPhase.Ended):
				{
					TouchEnded(touch);
					break;
				}

				case (TouchPhase.Canceled):
				{
					TouchCanceled(touch);
					break;
				}
			}
		}
		
		#region DEBUG
		#if UNITY_EDITOR
		// MOUSE DEBUGGING
		// NOT COMPILED IN BUILDS
		if (Input.touchCount == 0)
		{
			if (Input.GetMouseButton(0)) 
			{
				OnMouse();
			}

			else if (Input.GetMouseButtonUp(0))
			{
				OnMouseUp();
			}
		}

		CallMouseHeldObjects();
		#endif
		#endregion

        CallHeldObjects(touches);
	}

	private Touch[] GetTouches() 
	{
        //touchList = new Touch[Input.touchCount];
        for (int i = 0; i < Input.touchCount; i++) 
		{
			touchList[i] = Input.GetTouch(i);
		}
		return touchList;
	}

	private void TouchBegan(Touch touch) 
	{
		if (CastRayFromTouch(touch))
		{
			// Check if non-interactable (ground, water etc.)
			if (raycastHits[touch.fingerId].Value.collider.gameObject.CompareTag("Non-interactable"))
			{
				RaycastHit hit = raycastHits[touch.fingerId].Value;
				particleSpawner.Burst(hit.point);
				return;
			}
			// Check if the touch hit a holdable
			Holdable holdable = GetHoldable(raycastHits[touch.fingerId].Value);
			if (holdable) 
			{
				EventManager.GetInstance().CallEvent (CustomEvent.HoldBegin);

				holdable.OnTouchBegin(raycastHits [touch.fingerId].Value);
				heldThisFrame[touch.fingerId] = holdable;
				return;
			} 
			CheckSwipe(touch);
		}
	}

	private void TouchStationary(Touch touch)
	{
		heldThisFrame[touch.fingerId] = heldLastFrame[touch.fingerId];
    }

	private void TouchMoved(Touch touch) 
	{
		if (CastRayFromTouch(touch))
		{
			// Check if the touch hit a holdable
			Holdable holdable = GetHoldable(raycastHits[touch.fingerId].Value);
			if (holdable)
			{
				heldThisFrame[touch.fingerId] = holdable;
				if (heldThisFrame[touch.fingerId] != heldLastFrame[touch.fingerId])
				{
					heldThisFrame[touch.fingerId].OnTouchBegin(raycastHits[touch.fingerId].Value);
				}
			}
			CheckSwipe(touch);
		}
		if (heldLastFrame[touch.fingerId] && heldLastFrame[touch.fingerId] != heldThisFrame[touch.fingerId])
		{
			heldLastFrame[touch.fingerId].OnTouchReleased();
			EventManager.GetInstance().CallEvent(CustomEvent.HoldEnd);
		}
	}

	private void TouchEnded(Touch touch) 
	{
		if (heldLastFrame[touch.fingerId])
		{
			heldLastFrame[touch.fingerId].OnTouchReleased();
			EventManager.GetInstance().CallEvent(CustomEvent.HoldEnd);
		}

		EventManager.GetInstance().CallEvent(CustomEvent.SwipeEnded);
		
		touchPositions[touch.fingerId].Clear();
		raycastHits[touch.fingerId] = null;
	}

	private void TouchCanceled(Touch touch) 
	{
		if (heldLastFrame[touch.fingerId])
		{
			heldLastFrame[touch.fingerId].OnTouchReleased();
			EventManager.GetInstance().CallEvent(CustomEvent.HoldEnd);
		}

		touchPositions[touch.fingerId].Clear();
		raycastHits[touch.fingerId] = null;
	}

	private bool CastRayFromTouch(Touch touch)
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(touch.position);
		if (Physics.Raycast(ray, out hit, raycastDistance)) 
		{
			raycastHits[touch.fingerId] = hit;
			return true;
		}
		raycastHits[touch.fingerId] = null;
		return false;
	}

	private Vector3 RotateVector(Vector3 vector, Vector3 angles)
	{
		Quaternion rotation = Quaternion.Euler(angles.x, angles.y, angles.z);

		Matrix4x4 completeRotationMatrix = new Matrix4x4();
		completeRotationMatrix = Matrix4x4.Rotate(rotation);

		return completeRotationMatrix * new Vector4(vector.x, vector.y, vector.z, 0);	
	}

	private void CheckSwipe(Touch touch)
	{
		touchPositions[touch.fingerId].Add(touch.position);

		Vector3 firstPosition = touchPositions[touch.fingerId][0];
		Vector3 lastPosition = touchPositions[touch.fingerId][touchPositions[touch.fingerId].Count-1];

		Vector3 firstPoint = testingCamera.ScreenToWorldPoint(new Vector3(firstPosition.x, firstPosition.y, testingCamera.nearClipPlane));
		Vector3 lastPoint = testingCamera.ScreenToWorldPoint(new Vector3(lastPosition.x, lastPosition.y, testingCamera.nearClipPlane));

		Vector3 direction = lastPoint - firstPoint;
		
		swipeDirections.Add(direction);

		Swipeable swipeable = GetSwipeable(raycastHits[touch.fingerId].Value);

		// Check if the touch hit a swipable	
		if (swipeable)
		{
			swipeable.OnSwipe(raycastHits[touch.fingerId].Value, direction);
			touchPositions[touch.fingerId].Clear();
			touchPositions[touch.fingerId].Add(touch.position);
		}
		if (touchPositions[touch.fingerId].Count > 0)
		{
			EventArgument argument = new EventArgument();
			argument.vectorComponent = direction;
			argument.raycastComponent = raycastHits[touch.fingerId].Value;
			EventManager.GetInstance().CallEvent(CustomEvent.Swipe, argument);
		}
	}

	private Holdable GetHoldable(RaycastHit hit)
	{
		return hit.collider.GetComponent<Holdable>();
	}

	private Swipeable GetSwipeable(RaycastHit hit)
	{
		return hit.collider.GetComponent<Swipeable>();
	}

	private void CallHeldObjects(Touch[] touches)
	{
		// Check if holdables are held
		foreach (Touch touch in touches)
		{
			if (heldThisFrame[touch.fingerId])
			{
				if (heldThisFrame[touch.fingerId] == heldLastFrame[touch.fingerId])
				{
					heldThisFrame[touch.fingerId].timeHeld += Time.deltaTime;
					heldThisFrame[touch.fingerId].OnTouchHold(raycastHits[touch.fingerId].Value);
				}
				else
				{
					heldThisFrame[touch.fingerId].timeHeld = 0;
				}
			}
		}

		// Reset held holdables
		for (int i = 0; i < maxNumberTouches; i++)
		{
			heldLastFrame[i] = heldThisFrame[i];
			heldThisFrame[i] = null;
		}
	}
	#endregion

	#region DEBUG
	#if UNITY_EDITOR
	// MOUSE DEBUGGING
	// NOT COMPILED IN BUILDS
	private void CallMouseHeldObjects()
	{
		if (heldThisFrame[0])
		{
			if (heldLastFrame[0] == heldLastFrame[0])
			{
				heldThisFrame[0].timeHeld += Time.deltaTime;
				heldThisFrame[0].OnTouchHold(raycastHits[0].Value);
			}
			else
			{
				heldThisFrame[0].timeHeld = 0;
			}
		}
	}

	private void OnMouse()
	{
		Vector2 mousePos = Input.mousePosition;
		if (CastRayFromMousePos(mousePos))
		{
			// Check if the ray hit a holdable
			Holdable holdable = GetHoldable(raycastHits[0].Value);
			if (holdable)
			{
				heldThisFrame[0] = holdable;
				if (Input.GetMouseButtonDown(0))
				{
					holdable.OnTouchBegin(raycastHits[0].Value);
					EventManager.GetInstance().CallEvent(CustomEvent.HoldBegin);
				}

				foreach (Holdable lastFrameHoldable in heldLastFrame)
				{
					if (lastFrameHoldable && !lastFrameHoldable.Equals(holdable))
					{
						lastFrameHoldable.OnTouchReleased();
						EventManager.GetInstance().CallEvent(CustomEvent.HoldEnd);
					}
				}
			}
		}		
		else
		{
			foreach (Holdable lastFrameHoldable in heldLastFrame)
			{
				if (lastFrameHoldable)
				{
					lastFrameHoldable.OnTouchReleased();
					EventManager.GetInstance().CallEvent(CustomEvent.HoldEnd);
				}
			}
		}
	}

	private void OnMouseUp()
	{
		Vector2 mousePos = Input.mousePosition;
		if (CastRayFromMousePos(mousePos))
		{
			// Check if the ray hit a holdable
			Holdable holdable = GetHoldable(raycastHits[0].Value);
			if (holdable)
			{
				holdable.OnTouchReleased();
				EventManager.GetInstance().CallEvent(CustomEvent.HoldEnd);
			}
		}
	}

	private bool CastRayFromMousePos(Vector2 pos) 
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(pos);
		if (Physics.Raycast(ray, out hit, raycastDistance))
		{
			raycastHits[0] = hit;
			return true;
		}
		raycastHits[0] = null;
		return false;
	}
	#endif
	#endregion
}
