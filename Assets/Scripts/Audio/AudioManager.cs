	// Author: Kristian Riis 
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events; 

public class AudioManager : Singleton<AudioManager> {

	private EventManager eventManager;
	private Dictionary<string, bool> soundsBeingPlayed = new Dictionary<string, bool>();
	private uint eventID; 
	public string groundLayer;
	public float sfxVolume = 100; 
	public float musicVolume = 100; 
	private float swipePower; 

	void Awake()
	{
		eventManager = EventManager.GetInstance();
	}

	void Start()
	{
		groundLayer = "Swamp";
		AkSoundEngine.SetState ("Ambience", "MediumOpen"); 
		PlaySound("Play_Ambience"); 
	}

	//SFX
    public void SetSFXVolume(float newVolumn)
    {
        AkSoundEngine.SetRTPCValue("SFX_Volume", newVolumn * 100);
    }

	//Volume
    public void SetMusicVolume(float newVolumn)
    {
        AkSoundEngine.SetRTPCValue("Music_Volume", newVolumn * 100);
    }
		
	//Calls when ever listened event is triggered 
	void OnEnable () 
	{
		EventDelegate postEvent = Poster; 
		EventDelegate stopEvent = Stopper;
		EventDelegate changeScene = NewScene; 
		EventDelegate somethingSunk = SunkAction;
		EventDelegate somethingFall = FallAction; 
		EventDelegate foreshadow = ForeshadowPost; 
		// Mechanics
		eventManager.AddListener (CustomEvent.Swipe, postEvent); 
		eventManager.AddListener (CustomEvent.HoldBegin, postEvent); 
		eventManager.AddListener (CustomEvent.SwipeEnded, stopEvent); 
		eventManager.AddListener (CustomEvent.HoldEnd, stopEvent); 
		// Scene-management
		eventManager.AddListener (CustomEvent.ResetGame, stopEvent); 
		eventManager.AddListener (CustomEvent.LoadScene, postEvent); 
		eventManager.AddListener (CustomEvent.LoadScene, changeScene);
		// Events triggered 
		eventManager.AddListener (CustomEvent.SinkHasHappened, somethingSunk);
		eventManager.AddListener (CustomEvent.FallHasHappend, somethingFall); 
		eventManager.AddListener (CustomEvent.ForeshadowEventTriggered, foreshadow); 
		//Ritual events
		//eventManager.AddListener (CustomEvent.AppleFall, actionEvent);
	}
		
	//Event poster 
	void Poster(EventArgument argument)
	{
		//Swipe
		if (argument.eventComponent == CustomEvent.Swipe) 
		{
		swipePower = argument.vectorComponent.magnitude * 100; 
		AkSoundEngine.SetRTPCValue ("SwipePower", swipePower); 	
		PlaySoundWC ("Play_GG_SD_Swipe_1"); 
		}
		//Hold begin
		if (argument.eventComponent == CustomEvent.HoldBegin) 
		{
			PlaySoundWC ("Play_GG_SD_Sink_1");
		}
	}

	//Event stopper 
	void Stopper(EventArgument argument)
	{
		if (argument.eventComponent == CustomEvent.SwipeEnded) 
		{
			StopSound("Stop_GG_SD_Wind"); 
		}
		if (argument.eventComponent == CustomEvent.HoldEnd)
		{
			StopSound("Stop_GG_SD_Sink_1"); 
		}
		if (argument.eventComponent == CustomEvent.ResetGame) 
		{
			StopSound ("Stop_All"); 
		}
	}

	//Sinked objects 
	void SunkAction(EventArgument argument)
	{
		if(argument.stringComponent == "Tree")
		{
			PlaySoundWCOtherScript ("Play_GG_SD_Mud_Sink", argument.gameObjectComponent); 
		}
		else if(argument.stringComponent == "Rock")
		{
			PlaySoundWCOtherScript("Play_GG_SD_Mud_Sink", argument.gameObjectComponent); 
		}
		else if(argument.stringComponent == "SomethingElse")
		{
			//Play_GG_SD_Sink_PH Play_GG_SD_Mud_Sink
		}
	}

		//Fallen objects
		void FallAction(EventArgument argument)
		{
			if(argument.stringComponent == "Tree")
			{
			PlaySoundWCOtherScript ("Play_GG_SD_Tree_Fall", argument.gameObjectComponent); 
			}
			else if(argument.stringComponent == "Rock")
			{
			PlaySoundWCOtherScript("Play_GG_SD_Stone_Fall", argument.gameObjectComponent); 
			}
			else if(argument.stringComponent == "SomethingElse")
			{
				//Play_GG_SD_Sink_PH
			}
		}
		
	//Foreshadowing
	void ForeshadowPost(EventArgument argument)
	{
		//Travel of P 
			if(argument.stringComponent == "Scena2")
			{
				PlaySoundWC("Play_GG_FSD_2");
			}
			if(argument.stringComponent == "Scena4")
			{
				PlaySoundWC("Play_GG_FSD_4_1");
			}
			
		//Travel of O 
			if(argument.stringComponent == "Scena1")
			{
				PlaySoundWC("Play_GG_FSD_1");
			}
			if(argument.stringComponent == "Scena3")
			{
				PlaySoundWC ("Play_FSD_3"); 
			}
			if(argument.stringComponent == "Crossroad")
			{
			//crossroad 
			}
	}

	//Scene-loader 
	void NewScene(EventArgument argument)
	{
		if (argument.stringComponent == "TittleScreen" && argument.intComponent == -1) 
		{
			//Do this
			//print("CurrentSceneIs"+argument.stringComponent + argument.intComponent);
		}
		if (argument.stringComponent == "IntroCutScene" && argument.intComponent == -1) 
		{
			//Do this 
		}
		if (argument.stringComponent == "IntroCutscene" && argument.intComponent == -1) 
		{
			AkSoundEngine.SetState("Music", "IntroCutscene"); 
		}
		if (argument.stringComponent == "IntroLevel" && argument.intComponent == -1) 
		{
			//Do this
			AkSoundEngine.SetState("Music", "Intro"); 
			PlaySound("Play_Music_01"); 
		}
		if (argument.stringComponent == "Overture" && argument.intComponent == -1) 
		{
			//PlaySoundWC("Play_Overture"); 
			//print ("OVERTURE"); 
		}
		if (argument.stringComponent == "Crossroads" && argument.intComponent == -1) 
		{
			//Give udtryk, om at der skal træffes et valg (eventuelt relativ stilhed)  
			AkSoundEngine.SetState("Music", "Crossroad"); 
			//print ("Crossroad is current"); 
		}
		if (argument.stringComponent == "LiO1" && argument.intComponent == -1) 
		{
			AkSoundEngine.SetState("Music", "O"); 
		}
		if (argument.stringComponent == "LiP1" && argument.intComponent == -1) 
		{
			AkSoundEngine.SetState("Music", "P"); 
		}
		if (argument.stringComponent == "RitualEvent" && argument.intComponent == -1) 
		{
			//Mere spacey musik 
			//PlaySoundWC("Play_GG_SD_FSD_Shaman");
			//PlaySoundWC("Play_GG_FSD_2"); 

		}
		if (argument.stringComponent == "SeperationEvent" && argument.intComponent == -1) 
		{
			//Do this
			//AkSoundEngine.SetState("Music", "SC1A"); 
			//Musik, der udtrykker seperation/ensomhed/etc
		}
		if (argument.stringComponent == "BearEvent" && argument.intComponent == -1) 
		{
			//Do this
			//PlaySoundWC("Play_GG_SD_FSD_Bear");
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
		if (argument.stringComponent == "Restart") 
		{
			//Do this
			//Restart 
			StopSound ("Stop_All"); 
		}
	}

	//Footsteps + layer
	public void Footstep()
	{
		groundLayer = string.Concat ("", groundLayer, ""); 
		AkSoundEngine.SetSwitch ("FS", groundLayer, gameObject);  
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

	//Can add a gameobject
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

	//Play-function without stop-callback, can add a gameobject 
	public void PlaySoundOtherScript(string soundName, GameObject thisthisthis)
	{
		soundName = string.Concat ("", soundName, ""); 
		eventID = AkSoundEngine.PostEvent (soundName, thisthisthis); 
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

	//MENU functions 
	public void MenuFadeSoundDown()
	{
		PlaySound ("Menu_FadeVolumeDown"); 	
	}
	public void MenuFadeSoundUp()
	{
		PlaySound ("Menu_FadeVolumeUp"); 	
	}
	public void OnMenuClick()
	{
		PlaySound("Play_GG_Menu_Click"); 
	}
}