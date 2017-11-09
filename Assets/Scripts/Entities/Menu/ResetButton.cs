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
		EventManager.GetInstance ().CallEvent (CustomEvent.ResetGame); 
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
