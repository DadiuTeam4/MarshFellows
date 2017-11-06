// Author: Kristian Riis 
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHolder : MonoBehaviour {

	public AudioManager audioManager; 

	// Use this for initialization
	void Awake () 
	{
		audioManager = AudioManager.GetInstance(); 	
	}

	void FS()
	{
		audioManager.Footstep (); 
	}
}
