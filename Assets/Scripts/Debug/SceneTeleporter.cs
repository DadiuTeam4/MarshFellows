// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Events;

public class SceneTeleporter : MonoBehaviour 
{
	private Dropdown dropdown; 
	private EventManager eventManager;

	void Awake()
	{
		dropdown = GetComponent<Dropdown>();
	}

	void Start()
	{
		eventManager = EventManager.GetInstance();
		for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
		{
			dropdown.options.Add(new Dropdown.OptionData(SceneManager.GetSceneByBuildIndex(i).name));
		}
	}

	public void LoadScene()
	{
		EventArgument argument = new EventArgument();
		argument.stringComponent = dropdown.options[dropdown.value].text;
		argument.intComponent = dropdown.value;
		eventManager.CallEvent(CustomEvent.LoadScene, argument);
		print(dropdown.options[dropdown.value].text + " loading.");
	}
}
