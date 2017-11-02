//author : Kristian Riis 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events; 

public class AudioManager : MonoBehaviour {

	public EventManager eventManager;
	public bool soundIsBeingPlayed = false; 
	public uint eventID; 
	public string eventName;

	void Awake()
	{
		eventManager = EventManager.GetInstance();
	}

	void Start()
	{
		//if Scene is this...
		AkSoundEngine.PostEvent("Play_GG_Ambience_Open_1", gameObject); 
	}
		
	//Calls when ever listened event is triggered 
	void OnEnable () 
	{
		EventDelegate postEvent = Poster; 
		EventDelegate stopEvent = Stopper; 

		eventManager.AddListener (CustomEvent.Swipe, postEvent); 
		eventManager.AddListener (CustomEvent.HoldBegin, postEvent); 
		eventManager.AddListener (CustomEvent.HoldEnd, stopEvent); 
		eventManager.AddListener (CustomEvent.ShakeBegin, postEvent); 
		eventManager.AddListener (CustomEvent.ShakeEnd, stopEvent);
	}

	void Update () 
	{
	}

	//Posts event when events has been called 
	void Poster(EventArgument argument)
	{
		if (argument.eventComponent == CustomEvent.Swipe) 
		{
			if (!soundIsBeingPlayed)
			{
				PostSoundEventWCallback ("Play_GG_SD_Swipe_1"); 
			}	
		}
		if (argument.eventComponent == CustomEvent.HoldBegin) 
		{
			if (!soundIsBeingPlayed)
			{
				PostSoundEventWCallback ("Play_GG_SD_Sink_1"); 
			}	
		}
		if (argument.eventComponent == CustomEvent.ShakeBegin) 
		{
			if (!soundIsBeingPlayed)
			{
				PostSoundEventWCallback ("Play_GG_SD_Shake_1"); 
			}	
		}
	}

	//Event stopper 
	void Stopper(EventArgument argument)
	{
		if (argument.eventComponent == CustomEvent.HoldEnd) 
		{
			//AkSoundEngine.StopPlayingID (eventID);
			StopSoundEvent("Stop_GG_SD_Sink_1"); 
		}
		if (argument.eventComponent == CustomEvent.ShakeEnd) 
		{
			StopSoundEvent("Stop_GG_SD_Shake_1"); 
		}
	}

	//Checks if the events has stopped 
	void EventHasStopped(object in_cookie, AkCallbackType in_type, object in_info)
	{
		if (in_type == AkCallbackType.AK_EndOfEvent)
		{
			soundIsBeingPlayed = false; 
		}
	}
		
	void PostSoundEventWCallback(string soundEventName)
	{
		soundEventName = string.Concat ("", soundEventName, ""); 
		eventID = AkSoundEngine.PostEvent (soundEventName, gameObject, (uint)AkCallbackType.AK_EndOfEvent, EventHasStopped, 1); 
	}

	void PostSoundEvent(string soundName)
	{
		soundName = string.Concat ("", soundName, ""); 
		AkSoundEngine.PostEvent (soundName, gameObject); 
	}

	void StopSoundEvent(string stopEventName)
	{
		stopEventName = string.Concat ("", stopEventName, ""); 
		AkSoundEngine.PostEvent (stopEventName, gameObject); 
	}
}
