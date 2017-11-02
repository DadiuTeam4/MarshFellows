//Author: Kristian Riis 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventTrigger
{
	a, b, c
	
}

public class PostEvent : MonoBehaviour {
	public AudioManager audioManager; 
	public string eventName; 
	public EventTrigger current;

	// Use this for initialization
	void Start () 
	{
		
		audioManager.eventName = string.Concat ("", eventName, ""); 


	}
}
