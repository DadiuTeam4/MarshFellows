// Author: Jonathan
// Contributers: Kristian 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Events; 

public class CreditsButton : MonoBehaviour 
{
	public AudioManager audioManager; 

	void Start() 
	{
		audioManager = AudioManager.GetInstance(); 	
	}

	public void RunCredits()
	{
		Debug.Log("Thank you baby Jesus!");
		audioManager.OnMenuClick (); 
	}
}

