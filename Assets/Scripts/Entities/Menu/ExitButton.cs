// Author: You Wu
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour {

	public void EndGame()
	{
		Debug.Log("Exit!");
		Application.Quit();
	}
}
