// Author: Jonathan
// Contributers: Kristian Riis

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Events; 

public class SFXButton : MonoBehaviour 
{
	public AudioManager audioManager; 

	void Start() 
	{
		audioManager = AudioManager.GetInstance(); 	
	}

	public void OpenSFXManager()
	{
		audioManager.OnMenuClick (); 
	}
}
