// Author: Kristian Riis 
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkableObjectAudio : MonoBehaviour {

	public EventAudioTrigger eventAudioTrigger; 
	public ObjectType objectType; 
		
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
		
	public void PlaySinkableSound()
	{
		//Tree
		if (EventAudioTrigger.hasBeenSunk && ObjectType.tree) 
		{
			//play tree sunk sound
		}
		if (EventAudioTrigger.hasFallen && ObjectType.tree) 
		{
			//play tree fall sound
		}

		//Ice
		if (EventAudioTrigger.hasBeenSunk && ObjectType.ice) 
		{
			//play ice sunk sound
		}
		if (EventAudioTrigger.hasFallen && ObjectType.ice) 
		{
			//play ice fall sound
		}

		//Stone
		if (EventAudioTrigger.hasBeenSunk && ObjectType.stone) 
		{
			//play stone sunk sound
		}
		if (EventAudioTrigger.hasFallen && ObjectType.stone) 
		{
			//play stone fall sound
		}
	}
}
