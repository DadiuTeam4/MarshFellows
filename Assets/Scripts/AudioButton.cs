// Author: You Wu
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioButton : MonoBehaviour {

	private AudioManager audioManager;
	public Transform audioPanel;
	
	void Start () {
		audioManager = AudioManager.GetInstance();
	}
	
	public void ShowAudioPanel()
	{
		audioPanel.gameObject.SetActive(true);
		transform.parent.gameObject.SetActive(false);
		audioManager.OnMenuClick();
	}
}
