// // Author: Mathias Dam Hedelund
// // Contributors: 
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;
// using UnityEditor;

// using Events;

// public class SceneTeleporter : MonoBehaviour 
// {
// 	private Dropdown dropdown; 
// 	private EventManager eventManager;

// 	void Awake()
// 	{
// 		dropdown = GetComponent<Dropdown>();
// 	}

// 	void Start()
// 	{
// 		eventManager = EventManager.GetInstance();
// 		EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;

// 		string sceneName;
// 		string[] splitName;
// 		char[] seperator = {'/', '.'};

// 		for (int i = 0; i < scenes.Length; i++)
// 		{
// 			sceneName = scenes[i].path;
// 			splitName = sceneName.Split(seperator);
// 			sceneName = splitName[splitName.Length-2];
// 			dropdown.options.Add(new Dropdown.OptionData(sceneName));
// 		}
// 	}

// 	public void LoadScene()
// 	{
// 		EventArgument argument = new EventArgument();
// 		argument.stringComponent = dropdown.options[dropdown.value].text;
// 		argument.intComponent = dropdown.value;
// 		eventManager.CallEvent(CustomEvent.LoadScene, argument);
// 		print(dropdown.options[dropdown.value].text + " loading.");
// 	}
// }
