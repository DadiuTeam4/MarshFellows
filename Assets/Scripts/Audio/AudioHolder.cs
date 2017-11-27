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

	void FSO()
	{
		audioManager.PlaySoundOtherScript("Play_FS", gameObject);
	}

	void FSP()
	{
		audioManager.PlaySoundOtherScript("Play_FS", gameObject);
	}


	void DeerFS()
	{
		audioManager.PlaySoundOtherScript ("Play_Deer_FS", gameObject); 
	}

	void BearFS()
	{
		audioManager.PlaySoundOtherScript ("Play_Bear_FS", gameObject); 
	}
}