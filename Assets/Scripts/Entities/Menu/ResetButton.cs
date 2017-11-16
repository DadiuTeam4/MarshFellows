// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Events; 

public class ResetButton : MonoBehaviour 
{
	public void ResetScene()
	{
		//EventManager.GetInstance ().CallEvent (CustomEvent.ResetGame); 
		UnloadAllScenes();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

    void UnloadAllScenes() 
    {
		int c = SceneManager.sceneCount;
		for (int i = 0; i < c; i++)
		{
        	Scene scene = SceneManager.GetSceneAt (i);       
    	    SceneManager.UnloadSceneAsync (scene);
		}
	}
}
