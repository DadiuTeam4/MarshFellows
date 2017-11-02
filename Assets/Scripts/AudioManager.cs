// Author: Kristian Riis 
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events; 

public class AudioManager : MonoBehaviour {

	public EventManager eventManager;
	public bool soundIsBeingPlayed = false; 
	public uint eventID; 

	void Awake()
	{
		eventManager = EventManager.GetInstance();
	}

	void Start()
	{
		//If Scene is this...
		AkSoundEngine.PostEvent("Play_GG_Ambience_Open_1", gameObject); 
	}
		
	//Calls when ever listened event is triggered 
	void OnEnable () 
	{
		EventDelegate postEvent = Poster; 
		EventDelegate stopEvent = Stopper; 

		// Mechanics
		eventManager.AddListener (CustomEvent.Swipe, postEvent); 
		eventManager.AddListener (CustomEvent.HoldBegin, postEvent); 
		eventManager.AddListener (CustomEvent.HoldEnd, stopEvent); 
		eventManager.AddListener (CustomEvent.ShakeBegin, postEvent); 
		eventManager.AddListener (CustomEvent.ShakeEnd, stopEvent);

		//Ritual events
		eventManager.AddListener (CustomEvent.AppleFall, postEvent);
	}

	//Event poster 
	void Poster(EventArgument argument)
	{
		if (argument.eventComponent == CustomEvent.Swipe) 
		{
			PlaySoundWC ("Play_GG_SD_Swipe_1"); 
		}
		if (argument.eventComponent == CustomEvent.HoldBegin) 
		{
			PlaySoundWC ("Play_GG_SD_Sink_1"); 
		}
		if (argument.eventComponent == CustomEvent.ShakeBegin) 
		{
			PlaySoundWC ("Play_GG_SD_Shake_1"); 
		}
		if (argument.eventComponent == CustomEvent.AppleFall) 
		{
			PlaySoundWC ("Play_GG_SD_AppleDrop"); 
		}
	}

	//Event stopper 
	void Stopper(EventArgument argument)
	{
		if (argument.eventComponent == CustomEvent.HoldEnd) 
		{
			StopSound("Stop_GG_SD_Sink_1"); 
		}
		if (argument.eventComponent == CustomEvent.ShakeEnd) 
		{
			StopSound("Stop_GG_SD_Shake_1"); 
		}
	}

	//Play-function with stop-callback 
	void PlaySoundWC(string soundEventName)
	{
		soundEventName = string.Concat ("", soundEventName, ""); 
		eventID = AkSoundEngine.PostEvent (soundEventName, gameObject, (uint)AkCallbackType.AK_EndOfEvent, EventHasStopped, 1); 
		soundIsBeingPlayed = true; 
	}

	//Play-function without stop-callback 
	void PlaySound(string soundName)
	{
		soundName = string.Concat ("", soundName, ""); 
		eventID = AkSoundEngine.PostEvent (soundName, gameObject); 
		soundIsBeingPlayed = true; 
	}

	//Stop function
	void StopSound(string stopEventName)
	{
		if (!soundIsBeingPlayed) 
		{
			stopEventName = string.Concat ("", stopEventName, ""); 
			AkSoundEngine.PostEvent (stopEventName, gameObject); 
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
}