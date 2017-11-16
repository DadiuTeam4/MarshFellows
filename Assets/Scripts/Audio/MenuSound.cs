// Author: Kristian Riis 
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSound : MonoBehaviour {

	public AudioManager audioManager; 

	void Start()
	{
		audioManager = AudioManager.GetInstance(); 	
	}
		
	public void OnMenuClick()
	{
		audioManager.PlaySoundOtherScript ("Play_GG_Menu_Click", gameObject); 
	}
}
