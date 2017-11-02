
// Author: Mathias Dam Hedelund
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

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
	[HideInInspector] public EventArgument latestEventArgument;

	private State previousState;
	private float stateTimeElapsed;
	private int eventNumber;
	private EventDelegate eventOccurredCallbacks;
	

	#region DEBUG
	#if UNITY_EDITOR
	public bool aiDebugging = false;
	[HideInInspector] public string debugInfo;
	#endif
	#endregion

	private void Awake()
	{ 
		animator = GetComponent<Animator>();
		navigator = GetComponent<Navigator>();
	}

	private void Start()
	{
		triggeredEvents = new Dictionary<CustomEvent, bool>();
		eventArguments = new Dictionary<CustomEvent, EventArgument>();
		for (int i = 0; i < events.Length; i++) 
		{
			triggeredEvents.Add(events[i], false);

			EventDelegate action = EventCallback;
			eventOccurredCallbacks += action;
			EventManager.GetInstance().AddListener(events[i], action);
		}
	}

	public bool CheckEventOccured(CustomEvent eventName) 
	{
		bool eventOccured = triggeredEvents[eventName];
		triggeredEvents[eventName] = false;
		return eventOccured;
	}

	private void EventCallback(EventArgument eventArgument)
	{
		bool eventValue;
		if (triggeredEvents.TryGetValue(eventArgument.eventComponent, out eventValue)) 
		{
			triggeredEvents[eventArgument.eventComponent] = true;
			eventArguments[eventArgument.eventComponent] = eventArgument;
		}
		else
		{
			print("Event " + eventArgument.eventComponent + " does not exist");
		}
	}

	public void TransitionToState(State nextState)
	{
		if (nextState != currentState) 
		{
			previousState = currentState;
			currentState = nextState;
			OnExitState();
		}
	}

	public void ReturnToPreviousState() 
	{
		TransitionToState(previousState);
	}

	private void Update()
	{
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
		stateTimeElapsed += Time.deltaTime;
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

	public void LookAt(Vector3 position)
	{
		// TODO: Get look direction and convert to animation coordinates
	}

	public void ResetLook()
	{
		// TODO: Reset animation to origin
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
		debugInfo += ("Current state:\t" + currentState.name + "\n");
		debugInfo += ("Current waypoint:\t" + navigator.GetDestination() + "\n");
		debugInfo += ("\nTriggered events:\n");
		foreach (CustomEvent eventName in events)
		{
			debugInfo += eventName + ": " + triggeredEvents[eventName] + "\n";
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
