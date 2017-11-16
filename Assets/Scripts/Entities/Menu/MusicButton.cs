// Author: Jonathan
// Contributers: Kristian

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Events; 

public class MusicButton : MonoBehaviour 
{
	public AudioManager audioManager; 

	void Start() 
	{
		audioManager = AudioManager.GetInstance(); 	
	}
	public void OpenMusicManager()
	{
		Debug.Log("Vi skal have mere strøm!");
		audioManager.OnMenuClick (); 
	}
}
