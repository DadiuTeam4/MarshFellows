// Author: Itai Yavin
// Contributor:

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{
	public enum CustomEvent
	{
		None = 0,
        Swipe = 1,
        HoldBegin = 2,
        HoldEnd = 3,
        AppleFall = 4,
        SwipeEffectEnded = 5,
        SwipeEffectStarted = 6,
        SeparationScenarioEntered = 7, 
        RitualScenarioEntered = 8,
        DeerScenarioEntered = 9,
        BearScenarioEntered = 10,
        ScenarioEnded = 11,
        LoadScene = 12,
        ResetGame = 13,
        ScenarioInteracted = 14,
        HiddenByFog = 15,
		ScareDeerEvent = 16,
        OReachedByP = 17,
		SinkGround = 18,
		SinkHasHappened = 19,
		UnlockedItem = 20,
		SwipeEnded = 21,
		ForeshadowEventTriggered
    }

	public class EventArgument
	{
		public string stringComponent = "";
		public float floatComponent = 0.0f;
		public int intComponent = 0;
		public bool boolComponent = false;
		public Vector3 vectorComponent = new Vector3();
		public Vector3[] vectorArrayComponent = null;
		public CustomEvent eventComponent = CustomEvent.None;
		public RaycastHit raycastComponent = new RaycastHit();
		public GameObject gameObjectComponent;
	}
	
	public delegate void EventDelegate(EventArgument argument);

	public class EventManager : Singleton<EventManager> 
	{
		private Dictionary<CustomEvent, EventDelegate> listeners = new Dictionary<CustomEvent, EventDelegate>();

		public void AddListener(CustomEvent eventName, EventDelegate newListener)
		{
			if (listeners.ContainsKey(eventName))
			{
				listeners[eventName] += newListener;
			}
			else
			{
				listeners.Add(eventName, newListener);
			}
		}

		public bool RemoveListener(CustomEvent eventName, EventDelegate oldListener)
		{
			if (listeners.ContainsKey(eventName))
			{
				listeners[eventName] -= oldListener;
					
				return true;
			}

			return false;
		}

		public bool CallEvent(CustomEvent eventName)
		{
			return CallEvent(eventName, new EventArgument());
		}

		public bool CallEvent(CustomEvent eventName, EventArgument argument)
		{
            if (listeners.ContainsKey(eventName))
			{
				argument.eventComponent = eventName;
				EventDelegate eventDelegate;
				listeners.TryGetValue(eventName, out eventDelegate);
			
				eventDelegate(argument);

				return true;
			}

			return false;
		}
	}
}