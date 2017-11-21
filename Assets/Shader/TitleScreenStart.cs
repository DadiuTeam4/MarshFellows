// Author: Peter Jæger
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Events;
using UnityEngine.AI;

public class TitleScreenStart : MonoBehaviour 
{
	//public string CutsceneScene = "IntroCutScene"; 
	public bool firstPlay;
	public GameObject fogDeer;
	TeachFog tF;
	public float delay;
	float timer;
	public Camera camera;
	public GameObject GlobalCam;
	EventManager eventManager;
	private EventDelegate eventDelegate;

	public GameObject FogCurtain;

	private NavMeshAgent navMeshO;
	private NavMeshAgent navMeshP;
	private float speedAfterZone = 1.6f;

	
	// Use this for initialization
	void Start () {
	eventManager = EventManager.GetInstance();
	eventManager.AddListener(CustomEvent.ResetGame, Restarted);
	eventManager.AddListener(CustomEvent.HiddenByFog, HiddenTest);
	FogCurtain.GetComponent<FogCurtain>().enabled = false;
	tF = fogDeer.GetComponent<TeachFog>();

	firstPlay = GameStateManager.current.playedBefore;
	
	if(!firstPlay)
	{
		EventArgument argument = new EventArgument();
		argument.stringComponent="IntroLevel";
		argument.intComponent=1;
		eventManager.CallEvent(CustomEvent.LoadScene, argument);
	}

	if(firstPlay)
	{
	fogDeer.SetActive(false);
	tF.enabled = false;
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

			FogCurtain.GetComponent<FogCurtain>().enabled = true;
			

		}
		timer += Time.deltaTime;
	}

	public void Restarted(EventArgument argument)
	{
		firstPlay = false;
	}


	public void HiddenTest(EventArgument argument)
	{
		
		eventManager.CallEvent(CustomEvent.ScenarioEnded);

		navMeshO = GameObject.Find("O").GetComponent<NavMeshAgent>();
		navMeshP = GameObject.Find("P").GetComponent<NavMeshAgent>();

		navMeshO.speed = speedAfterZone;
		navMeshP.speed = speedAfterZone;
						
	}
}
