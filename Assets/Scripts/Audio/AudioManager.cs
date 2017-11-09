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
		groundLayer = "Swamp";
		PlaySound ("Play_Music_1"); 
	}
		
	//Calls when ever listened event is triggered 
	void OnEnable () 
	{
		EventDelegate postEvent = Poster; 
		EventDelegate stopEvent = Stopper;
		EventDelegate changeScene = NewScene; 
	
		// Mechanics
		eventManager.AddListener (CustomEvent.Swipe, postEvent); 
		eventManager.AddListener (CustomEvent.HoldBegin, postEvent); 
		eventManager.AddListener (CustomEvent.HoldEnd, stopEvent); 
		eventManager.AddListener (CustomEvent.ResetGame, stopEvent); 
		eventManager.AddListener (CustomEvent.LoadScene, postEvent); 
		eventManager.AddListener (CustomEvent.LoadScene, changeScene);

		//Ritual events
		//eventManager.AddListener (CustomEvent.AppleFall, actionEvent);
	}
		
	//Event poster 
	void Poster(EventArgument argument)
	{
		//Swipe
		if (argument.eventComponent == CustomEvent.Swipe) 
		{
			PlaySoundWC ("Play_GG_SD_Swipe_1"); 
		}
		//Hold begin
		if (argument.eventComponent == CustomEvent.HoldBegin) 
		{
			PlaySoundWC ("Play_GG_SD_Sink_1");
		}
		//Apple 
		if (argument.eventComponent == CustomEvent.AppleFall) 
		{
			PlaySoundWC ("Play_GG_SD_AppleDrop"); 
		}	
	}

	//Scene-manager 
	void NewScene(EventArgument argument)
	{
		if (argument.stringComponent == "TittleScreen" && argument.intComponent == -1) 
		{
			//Do this
			//print("CurrentSceneIs"+argument.stringComponent + argument.intComponent);
		}
		if (argument.stringComponent == "IntroLevel" && argument.intComponent == -1) 
		{
			//Do this
			PlaySound("Play_GG_Ambience_Open_1"); 
		}
		if (argument.stringComponent == "Overture" && argument.intComponent == -1) 
		{
			//Do this
			//Play overture 
		}
		if (argument.stringComponent == "Crossroad" && argument.intComponent == -1) 
		{
			//Do this
			//Give udtryk, om at der skal træffes et valg (eventuelt relativ stilhed)  
		}
		if (argument.stringComponent == "RitualEvent" && argument.intComponent == -1) 
		{
			//Do this
			//Mere spacey musik 
		}
		if (argument.stringComponent == "SeperationEvent" && argument.intComponent == -1) 
		{
			//Do this
			//Musik, der udtrykker seperation/ensomhed/etc
		}
		if (argument.stringComponent == "BearEvent" && argument.intComponent == -1) 
		{
			//Do this
		}
		if (argument.stringComponent == "DeerEvent" && argument.intComponent == -1) 
		{
			//Do this
		}
		if (argument.stringComponent == "BeachEvent" && argument.intComponent == -1) 
		{
			//Do this
			//End music 
		}
	}
		
	//Event stopper 
	void Stopper(EventArgument argument)
	{
		if (argument.eventComponent == CustomEvent.HoldEnd) 
		{
			StopSound("Stop_GG_SD_Sink_1"); 
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

	public void PlaySoundWCOtherScript(string soundEventName, GameObject thisthis)
	{
		soundEventName = string.Concat("", soundEventName, "");
		bool isSoundPlaying;
		soundsBeingPlayed.TryGetValue(soundEventName, out isSoundPlaying);
		if (!isSoundPlaying)
		{
			AkSoundEngine.PostEvent(soundEventName, thisthis, (uint)AkCallbackType.AK_EndOfEvent, EventHasStopped, soundEventName);
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