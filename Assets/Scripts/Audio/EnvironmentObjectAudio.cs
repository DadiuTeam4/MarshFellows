// Author: Kristian Riis 
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentObjectAudio : MonoBehaviour {

	//public EventAudioTrigger eventAudioTrigger; 
	public ObjectType objectType; 
	public AudioManager audioManager; 

	void Awake()
	{
		audioManager = AudioManager.GetInstance(); 	
	}
	/*
	public enum EventAudioTrigger
	{
		hasBeenSunk,
		hasFallen
	}
	*/

	public enum ObjectType
	{
		tree, 
		stone,
		ice
	}
		
	public void PlayEnvironmentFallAudio()
	{
		//EvenTrigger : Fallen 
		//if (eventAudioTrigger == EventAudioTrigger.hasFallen) 
 {		//ObjectType
			if (objectType == ObjectType.tree) {
				//play tree fall sound
				audioManager.PlaySoundWCOtherScript ("PlayTree", gameObject); 
				print ("Fallen"); 
			}
			if (objectType == ObjectType.stone) {
				//play stone fall sound
			}
			if (objectType == ObjectType.ice) {
				//play ice fall sound
			}
		}
	}

		public void PlayEnvironmentSunkAudio()
		{
			if (objectType == ObjectType.tree) 
			{
				//play tree sunk sound
			print ("Sunken"); 
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
