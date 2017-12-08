
// Author: Mathias Dam Hedelund
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace CharacterBehaviour
{
	[RequireComponent(typeof(Navigator))]
	public class StateController : MonoBehaviour
	{
		public CustomEvent[] events;
		public State currentState;
		public bool active = true;

		[HideInInspector] public Navigator navigator;
		[HideInInspector] public Animator animator;
		[HideInInspector] public Dictionary<CustomEvent, bool> triggeredEvents;
		[HideInInspector] public Dictionary<CustomEvent, EventArgument> eventArguments;
		[HideInInspector] public EventManager eventManager;
		[HideInInspector] public EventArgument latestEventArgument;

		private IkHandler ikHandler;
		[HideInInspector] public Transform lookAtTarget; 

		private State previousState;
		private float stateTimeElapsed;
		private int eventNumber;
		private EventDelegate eventOccurredCallbacks;
		private EventDelegate locationEventCallback;

		#region DEBUG
	#if UNITY_EDITOR
		public bool aiDebugging = false;
		[HideInInspector] public string debugInfo;
		#endif
		#endregion

		private void Awake()
		{ 
			animator = GetComponentInChildren<Animator>();
			navigator = GetComponent<Navigator>();
			ikHandler = GetComponentInChildren<IkHandler>();
		}

		private void Start()
		{
			eventManager = EventManager.GetInstance();
			triggeredEvents = new Dictionary<CustomEvent, bool>();
			eventArguments = new Dictionary<CustomEvent, EventArgument>();
			foreach (CustomEvent e in System.Enum.GetValues(typeof(CustomEvent)))
			{
				triggeredEvents.Add(e, false);

				EventDelegate action = EventCallback;
				eventOccurredCallbacks += action;
				eventManager.AddListener(e, action);
			}
			locationEventCallback = SetLocationToLookAt;
			eventManager.AddListener(CustomEvent.BroadcastObjectLocation, locationEventCallback);
		}



		public bool CheckEventOccured(CustomEvent eventName) 
		{
			bool eventOccured;
			triggeredEvents.TryGetValue(eventName, out eventOccured);

			//if(eventOccured)
			//{
			//    Debug.Log("Checking " + eventName + " to be true, in current state: " + currentState);
			//}

			triggeredEvents[eventName] = false;
			return eventOccured;
		}

		private void EventCallback(EventArgument eventArgument)
		{
			triggeredEvents[eventArgument.eventComponent] = true;
			eventArguments[eventArgument.eventComponent] = eventArgument;
		}

		private void SetLocationToLookAt(EventArgument eventArgument)
		{
			lookAtTarget = eventArgument.gameObjectComponent.transform;
		}

		public void TransitionToState(State nextState)
		{
			if (nextState != currentState) 
			{
				ClearEventState();
				ResetStateTimer();
				previousState = currentState;
				currentState.OnStateExit(this);
				OnExitState();
				currentState = nextState;
				currentState.OnStateEnter(this);
			}
		}

		private void ClearEventState()
		{
			List<CustomEvent> keys = new List<CustomEvent>();
			foreach(var item in triggeredEvents)
			{
				keys.Add(item.Key);
			}
			for(int i = 0; i < keys.Count; i++)
			{
				triggeredEvents[keys[i]] = false;
			}
		}

		public void ReturnToPreviousState() 
		{
			TransitionToState(previousState);
		}

		private void Update()
		{
			stateTimeElapsed += Time.deltaTime;
			if (!active) 
			{
				return;
			}
			#region DEBUG
			#if UNITY_EDITOR
			if (aiDebugging)
			{
				debugInfo = "";
				UpdateDebugInfo();
			}
	#endif
			#endregion
			//animator.SetFloat("speed", navigator.GetSpeed());

			currentState.UpdateState(this);
		}

		public bool CheckIfCountDownElapsed(float duration)
		{
			return stateTimeElapsed >= duration;
		}

		public void ResetStateTimer() 
		{
			stateTimeElapsed = 0;
		}

		private void OnExitState()
		{
			ResetStateTimer();
		}

		public Transform GetDestination()
		{
			return navigator.GetDestination();
		}

		public void LookAt(bool lookForward)
		{
			// TODO: Get look direction and convert to animation coordinates
			//animator.SetFloat("reactDirection", n);
			if (lookForward)
			{
				ikHandler.LookForward();
			} else
			{
				ikHandler.LookAtTarget(lookAtTarget);
			}
		}

		public void ResetLook()
		{
			// TODO: Reset animation to origin
		}

		public void SetAnimatorBool(string s, bool b)
		{
			animator.SetBool(s, b);
		}

		public void SetAnimatorFloat(string s, float f)
		{
			animator.SetFloat(s, f);
		}

		public void SetLatestEventArguments(EventArgument eventArgument)
		{
			latestEventArgument = eventArgument;
		}

		#region DEBUG
		#if UNITY_EDITOR
		private void UpdateDebugInfo()
		{
			debugInfo += ("State time elapsed:\t" + stateTimeElapsed + "\n");
			debugInfo += ("Current state:\t" + currentState + "\n");
			debugInfo += ("Current waypoint:\t" + navigator.GetDestination() + "\n");
			debugInfo += ("Current splitwaypoint:\t" + navigator.GetSplitWaypoint() + "\n");
			debugInfo += ("\nTriggered events:\n");
			foreach (CustomEvent eventName in System.Enum.GetValues(typeof(CustomEvent)))
			{
				if (triggeredEvents[eventName])
				{
					debugInfo += eventName + ": " + triggeredEvents[eventName] + "\n";
				}
			}
		}

		void OnGUI()
		{
			if (aiDebugging)
			{
				GUI.Box(new Rect(0, 0, Screen.width, Screen.height), debugInfo, "");
			}
		}
		#endif
		#endregion
	}
}