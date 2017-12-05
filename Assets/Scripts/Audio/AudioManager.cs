	// Author: Kristian Riis 
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events; 

public class AudioManager : Singleton<AudioManager> {

	private GlobalConstantsManager constantsManager;
	private EventManager eventManager;
	private Dictionary<string, bool> soundsBeingPlayed = new Dictionary<string, bool>();
	private uint eventID; 
	public string groundLayer;
	public float sfxVolume; 
	public float musicVolume; 
	private float swipePower; 

	//Fade function 
	private float fadeVolValue = 50f;
	private float fadeMax = 100f;
	private float fadeMin = 0f; 
	private float duration;  

	//Ritual 
	private bool isDisrupted = false; 

	void Awake()
	{
		constantsManager = GlobalConstantsManager.GetInstance();
		sfxVolume = constantsManager.constants.sfxVolume; 
		musicVolume = constantsManager.constants.musicVolume;

		eventManager = EventManager.GetInstance();
	}

	void Start()
	{
		//groundLayer = "FS_Forrest";
		AkSoundEngine.SetSwitch ("FS_Forrest", groundLayer, gameObject);  
		AkSoundEngine.SetState ("Ambience", "OpenFew"); 
		AkSoundEngine.SetState ("ShamanDrum", "Normal"); 
		PlaySound("Play_Ambience"); 
		isDisrupted = false; 
	}

	void Update()
	{
		AkSoundEngine.SetRTPCValue ("MusicFadeVolume", fadeVolValue); 
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
		EventDelegate audioTriggered = PlacementTrigger; 
		EventDelegate somethingSunk = SunkAction;
		EventDelegate somethingFall = FallAction; 
		EventDelegate foreshadow = ForeshadowPost; 
		// Mechanics
		eventManager.AddListener (CustomEvent.Swipe, postEvent); 
		eventManager.AddListener (CustomEvent.HoldBegin, postEvent); 
		eventManager.AddListener (CustomEvent.SwipeEnded, stopEvent); 
		eventManager.AddListener (CustomEvent.HoldEnd, stopEvent); 
		//Ritual
		eventManager.AddListener (CustomEvent.RitualDisrupted, postEvent); 
		eventManager.AddListener (CustomEvent.ScenarioInteracted, postEvent); 
		// Scene-/Location-management
		eventManager.AddListener (CustomEvent.ResetGame, stopEvent); 
		eventManager.AddListener (CustomEvent.AudioTrigger, audioTriggered); 
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
		if (argument.eventComponent == CustomEvent.RitualDisrupted) 
		{
			if (!isDisrupted) 
			{
				AkSoundEngine.SetState ("ShamanDrum", "Disrupt"); 
				AkSoundEngine.SetState ("Music", "RitualDisrupt"); 
				PlaySound ("Play_GG_SD_Shaman_Disrupt"); 
				isDisrupted = true; 
			}
		}
		if (argument.eventComponent == CustomEvent.ScenarioInteracted && argument.stringComponent == "Ritual") 
		{
			AkSoundEngine.SetState("Music", "RitualFlight"); 
			PlaySoundWC ("Play_GG_SD_Revealed"); 
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
			//PlaySoundWCOtherScript ("Play_GG_SD_Mud_Sink", argument.gameObjectComponent); 
		}
		else if(argument.stringComponent == "Rock")
		{
			//PlaySoundWCOtherScript("Play_GG_SD_Mud_Sink", argument.gameObjectComponent); 
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
			if(argument.stringComponent == "Crossroad")
			{
				PlaySoundWC("Play_GG_SD_DEER_Approach"); 
			}
			
		//Travel of O 
			if(argument.stringComponent == "Scena1")
			{
				PlaySoundWC("Play_GG_FSD_1");
			}
//			if(argument.stringComponent == "Scena3")
//			{
//				PlaySoundWC ("Play_GG_FSD_3"); 
//			}

//			if(argument.stringComponent == "Missout1")
//			{
//			PlaySoundWC ("Play_GG_FSD_Choir"); 
//			}
//			if(argument.stringComponent == "Missout2")
//			{
//			PlaySoundWC ("Play_GG_FSD_Sbhaman_Drum"); 
//			}
	}

	//AudioTrigger//Location 
	void PlacementTrigger(EventArgument argument)
	{
		//Ambience-switches 
		if (argument.stringComponent == "ByWater") 
		{
			AkSoundEngine.SetState ("Ambience", "ByWater"); 
		}
		if (argument.stringComponent == "BetweenForrest") 
		{
			AkSoundEngine.SetState ("Ambience", "BetweenForrest"); 
		}
		if (argument.stringComponent == "Open") 
		{
			AkSoundEngine.SetState ("Ambience", "MediumOpen"); 
		}

		//Music triggers        
		//Debug.Log(argument.stringComponent);

		//Intro
		if (argument.stringComponent == "TittleScreen") 
		{
			//
		}
		if (argument.stringComponent == "IntroStinger") 
		{
			PlaySound("Play_StingerIntro"); 
		}
		if (argument.stringComponent == "IntroCutscene") 
		{
			AkSoundEngine.SetState("Music", "IntroCutscene"); 
		}
		if (argument.stringComponent == "IntroLevel") 
		{
			AkSoundEngine.SetState("Music", "Intro"); 
			PlaySound("Play_Music_01"); 
			StartCoroutine (FadeIn ()); 
		}

		//Ritual Scenario
		if (argument.stringComponent == "RitualSurvived") 
		{
			AkSoundEngine.SetState("Music", "RitualSurvived"); 
		}
		if (argument.stringComponent == "Ritual") 
		{
			AkSoundEngine.SetState("Music", "Ritual"); 
		}
		if (argument.stringComponent == "WhisperPlay") 
		{
			//Restart
			PlaySoundWC("Play_GG_SD_SHAMAN_WHISPER");  
		}

		//Crossroad
		if (argument.stringComponent == "Crossroad") 
		{
			AkSoundEngine.SetState("Music", "Crossroad"); 
		}
			
		//Deer Scenario
		if (argument.stringComponent == "DeerIntro") 
		{
			AkSoundEngine.SetState("Music", "DeerIntro"); 
		}
		if (argument.stringComponent == "Deer") 
		{
			//
			AkSoundEngine.SetState("Music", "Deer"); 
		}

		//Bear Scenario 
		if (argument.stringComponent == "Bear") 
		{
			//
			AkSoundEngine.SetState("Music", "P"); 
		}
		if (argument.stringComponent == "B") 
		{
			//
		}

		//Restart
		if (argument.stringComponent == "Restart") 
		{
			//Restart
			StopSound ("Stop_All"); 
		}
	}

	//Footsteps + layer
	public void FootstepO()
	{
		groundLayer = string.Concat ("", groundLayer, ""); 
	}

	public void FootstepP()
	{
		groundLayer = string.Concat ("", groundLayer, ""); 
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
		PlaySound ("Pause_Music_01"); 
		PlaySound ("Pause_Ambience"); 
	}
	public void MenuFadeSoundUp()
	{
		PlaySound ("Menu_FadeVolumeUp"); 
		PlaySound ("Resume_Music_01"); 
		PlaySound ("Resume_Ambience"); 
	}
	public void OnMenuClick()
	{
		PlaySound("Play_GG_Menu_Click"); 
	}

	IEnumerator FadeIn()
	{
		duration = 4.5f * Time.deltaTime; 
		while (fadeVolValue < fadeMax) {
			fadeVolValue += duration; 
			yield return null; 
		}
	}
}