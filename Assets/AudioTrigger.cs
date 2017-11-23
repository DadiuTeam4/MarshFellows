//Author:Tilemachos
//Co-author:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
public class AudioTrigger : MonoBehaviour {

	private bool haveBeenTriggered = false;
	public string locationOFTrigger = "";
	void OnTriggerEnter(Collider other)
	{
		if(!haveBeenTriggered && (other.gameObject.tag == "O" || other.gameObject.tag == "P" || other.gameObject.tag == "ScenarioTrigger"))
		{
			EventManager eventManager = EventManager.GetInstance(); 
    
			EventArgument argument = new EventArgument();
			
			argument.stringComponent = locationOFTrigger;
			
			eventManager.CallEvent(CustomEvent.AudioTrigger,argument);

		}
	}


}
