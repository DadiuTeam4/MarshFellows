// Author: Peter Jæger
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Events;

public class TitleScreenStart : MonoBehaviour 
{
	public string CutsceneScene = "IntroCutScene"; 
	public bool firstPlay;
	public GameObject fogDeer;
	TeachFog tF;
	public float delay;
	float timer;
	public Camera camera;
	EventManager eventManager;
	private EventDelegate eventDelegate;


	
	// Use this for initialization
	void Start () {
	eventManager = EventManager.GetInstance();
	eventDelegate = Restarted;
	eventManager.AddListener(CustomEvent.ResetGame, eventDelegate);
	
	tF = fogDeer.GetComponent<TeachFog>();
	
	if(firstPlay)
	{
	fogDeer.SetActive(false);
	tF.enabled = false;
	camera.enabled = false;
	}

	timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(firstPlay && timer > delay)
		{
			fogDeer.SetActive(true);
			tF.enabled = true;
			firstPlay = false;
			SceneManager.UnloadSceneAsync (CutsceneScene);
			camera.enabled = true;

		}
		timer += Time.deltaTime;
	}

	public void Restarted(EventArgument argument)
	{
		firstPlay = false;
	}
}
