// Author: Kristian Riis 
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events; 

public class AudioManager : Singleton<AudioManager> {

	public EventManager eventManager;
    public Dictionary<string, bool> soundsBeingPlayed = new Dictionary<string, bool>();
	public uint eventID; 
	public string groundLayer; 

	void Awake()
	{
		eventManager = EventManager.GetInstance();
	}

	void Start()
	{
		//If Scene is this...
		AkSoundEngine.PostEvent("Play_GG_Ambience_Open_1", gameObject);
		groundLayer = "Swamp";
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
		eventManager.AddListener (CustomEvent.ResetGame, stopEvent); 

		//Ritual events
		//eventManager.AddListener (CustomEvent.AppleFall, actionEvent);
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
		if (argument.eventComponent == CustomEvent.ResetGame) 
		{
			StopSound ("Stop_All"); 
		}

	}

	public void Footstep()
	{
		groundLayer = string.Concat ("", groundLayer, ""); 
		AkSoundEngine.SetSwitch ("FS", groundLayer, gameObject);  
		PlaySound ("Play_FS");
	}

	//Play-function with stop-callback to a specific event  
	void PlaySoundWC(string soundEventName)
	{
        soundEventName = string.Concat("", soundEventName, "");
        bool isSoundPlaying;
        soundsBeingPlayed.TryGetValue(soundEventName, out isSoundPlaying);
        if (!isSoundPlaying)
        {
            eventID = AkSoundEngine.PostEvent(soundEventName, gameObject, (uint)AkCallbackType.AK_EndOfEvent, EventHasStopped, soundEventName);
            soundsBeingPlayed[soundEventName] = true;
        }
	}

	//Play-function without stop-callback 
	void PlaySound(string soundName)
	{
		soundName = string.Concat ("", soundName, ""); 
		eventID = AkSoundEngine.PostEvent (soundName, gameObject); 
	}

	//Stop function
	void StopSound(string stopEventName)
	{
			stopEventName = string.Concat ("", stopEventName, ""); 
			AkSoundEngine.PostEvent (stopEventName, gameObject); 
	}
		
	//Checks if the specific event has stopped 
	void EventHasStopped(object in_cookie, AkCallbackType in_type, object in_info)
	{
        string soundEventName = (string) in_cookie;
		if (in_type == AkCallbackType.AK_EndOfEvent)
		{
            soundsBeingPlayed[soundEventName] = false;
        }
    }
}