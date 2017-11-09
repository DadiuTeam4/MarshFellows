﻿// Author: Kristian Riis 
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentObjectAudio : MonoBehaviour {

	public EventAudioTrigger eventAudioTrigger; 
	public ObjectType objectType; 
	public AudioManager audioManager; 

	void Awake()
	{
		audioManager = AudioManager.GetInstance(); 	
	}

	public enum EventAudioTrigger
	{
		hasBeenSunk,
		hasFallen
	}

	public enum ObjectType
	{
		tree, 
		stone,
		ice
	}
		
	public void PlayEnvironmentAudio()
	{
		//Fall 
		if (eventAudioTrigger == EventAudioTrigger.hasFallen) 
		{
			if (objectType == ObjectType.tree) 
			{
				//play tree fall sound
				//AudioManager.PlaySoundWC("name"); 
			}
			if (objectType == ObjectType.stone) 
			{
				//play stone fall sound
			}
			if (objectType == ObjectType.ice) 
			{
				//play ice fall sound
			}
		}

		//Sunk 
		if (eventAudioTrigger == EventAudioTrigger.hasBeenSunk) 
		{
			if (objectType == ObjectType.tree) 
			{
				//play tree sunk sound
			}
			if (objectType == ObjectType.stone) 
			{
				//play stone sunk sound
			}
			if (objectType == ObjectType.ice) 
			{
				//play ice sunk sound
			}
		}
	}
}