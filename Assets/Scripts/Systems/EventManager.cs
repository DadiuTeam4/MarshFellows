// Author: Itai Yavin
// Contributor:

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{
	public enum CustomEvent
	{
		None,
		Swipe,
		ShakeBegin,
		ShakeEnd,
		HoldBegin,
		HoldEnd,
		AppleFall,
		SwipeEffectEnded, 
    	SwipeEffectStarted,
		SeparationScenarioTriggered,
		RitualScenarioTriggered,
		DeerScenarioTriggered,
		BearScenarioTriggered,
		ScenarioEnded,
		LoadScene,
		ResetGame
	}

	public class EventArgument
	{
		public string stringComponent = "";
		public float floatComponent = 0.0f;
		public int intComponent = 0;
		public Vector3 vectorComponent = new Vector3();
		public Vector3[] vectorArrayComponent = null;
		public CustomEvent eventComponent = CustomEvent.None;
		public RaycastHit raycastComponent = new RaycastHit();
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