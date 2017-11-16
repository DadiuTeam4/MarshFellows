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
	public GameObject creditsPanel;
	private GameObject instance;

	void Start() 
	{
		audioManager = AudioManager.GetInstance(); 	
	}
	
	public void SpawnPanel() 
	{
		if (!instance)
		{
			instance = Instantiate(creditsPanel);
			audioManager.OnMenuClick ();
		}
		else 
		{
			instance.SetActive(!instance.active);
			audioManager.OnMenuClick (); 
		}
	}
}

