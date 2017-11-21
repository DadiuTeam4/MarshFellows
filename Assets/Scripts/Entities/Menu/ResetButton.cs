// Author: Itai Yavin
// Contributors: Kristian 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Events; 

public class ResetButton : MonoBehaviour 
{
	public AudioManager audioManager; 

	void Start() 
	{
		audioManager = AudioManager.GetInstance(); 	
	}

	public void ResetScene()
	{
		EventManager.GetInstance ().CallEvent (CustomEvent.ResetGame); 
		audioManager.OnMenuClick (); 
		UnloadAllScenes();
		SceneManager.LoadScene("GlobalScene");
	}

    void UnloadAllScenes() 
    {
		ChangeSceneEmitter.sceneIndex = 1;
		int c = SceneManager.sceneCount;
		for (int i = 0; i < c; i++)
		{
        	Scene scene = SceneManager.GetSceneAt (i);       
    	    SceneManager.UnloadSceneAsync (scene);
		}
	}
}
