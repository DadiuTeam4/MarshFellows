//author : Kristian Riis 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events; 

public class AudioManager : MonoBehaviour {

	public string eventName; 
	public EventManager eventManager;

	void Awake()
	{
		eventManager = EventManager.GetInstance();
	}

	void Start()
	{
		//if Scene is this...
		AkSoundEngine.PostEvent("Play_GG_Ambience_Open_1", gameObject); 
	}
		
	// Use this for initialization
	void OnEnable () 
	{
		//eventManager.AddListener (CustomEvent.test); 
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void PostEvent(EventArgument argument)
	{
		AkSoundEngine.PostEvent (eventName, gameObject); 
	}
}
