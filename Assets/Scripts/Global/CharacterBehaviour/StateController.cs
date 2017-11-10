
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
    [HideInInspector] public EventManager eventManager;
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
		animator = GetComponentInChildren<Animator>();
		navigator = GetComponent<Navigator>();
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
 /*       for (int i = 0; i < events.Length; i++) 
		{
			triggeredEvents.Add(events[i], false);

			EventDelegate action = EventCallback;
			eventOccurredCallbacks += action;
			EventManager.GetInstance().AddListener(events[i], action);
		} */
	}

	public bool CheckEventOccured(CustomEvent eventName) 
	{
        bool eventOccured;
        triggeredEvents.TryGetValue(eventName, out eventOccured);

		triggeredEvents[eventName] = false;
		return eventOccured;
	}

	private void EventCallback(EventArgument eventArgument)
	{
        //print("callback " + eventArgument.eventComponent);
        triggeredEvents[eventArgument.eventComponent] = true;
        eventArguments[eventArgument.eventComponent] = eventArgument;
        foreach (KeyValuePair<CustomEvent, bool> kvp in triggeredEvents)
        {
            //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            //Debug.Log("Key = " + kvp.Key + ", Value = " + kvp.Value);
        }
    }

	public void TransitionToState(State nextState)
	{
		if (nextState != currentState) 
		{
			previousState = currentState;
            OnExitState();
            currentState = nextState;
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

	public void LookAt(float n)
	{
        // TODO: Get look direction and convert to animation coordinates
        animator.SetFloat("reactDirection", n);

	}

    public void SetAnimatorBool(string s, bool b)
    {
        animator.SetBool(s, b);
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
		debugInfo += ("Current state:\t" + currentState + "\n");
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
