// Author: Mathias Dam Hedelund
// Contributors: Itai Yavin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

using Events;

public class SceneTeleporter : MonoBehaviour 
{
    private Dropdown dropdown; 
	private EventManager eventManager;

    [SerializeField]
    private List<GameObject> hunters;
    [SerializeField]
    private Dictionary<string, Vector3> teleportWaypoints = new Dictionary<string, Vector3>();

	void Awake()
	{
		dropdown = GetComponent<Dropdown>();
	}

	void Start()
	{
        Setup();
	}

	public void LoadScene()
	{
		EventArgument argument = new EventArgument();
		argument.stringComponent = dropdown.options[dropdown.value].text;
		argument.intComponent = dropdown.value;
		eventManager.CallEvent(CustomEvent.LoadScene, argument);
		print(dropdown.options[dropdown.value].text + " loading.");

        foreach (GameObject hunter in hunters)
        {
            Vector3 position;
            teleportWaypoints.TryGetValue(argument.stringComponent, out position);
            hunter.transform.position = position;
        }
	}

    public void Setup()
    {
		eventManager = EventManager.GetInstance();
		EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;

		string sceneName;
		string[] splitName;
		char[] seperator = {'/', '.'};

		for (int i = 0; i < scenes.Length; i++)
		{
			sceneName = scenes[i].path;
			splitName = sceneName.Split(seperator);
			sceneName = splitName[splitName.Length-2];
			dropdown.options.Add(new Dropdown.OptionData(sceneName));
		}

        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("DebuggingWaypoint");
        GameObject[] hunterObjects = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject waypoint in waypoints)
        {
            teleportWaypoints.Add(waypoint.scene.name, waypoint.transform.position);
            Debug.Log(waypoint.scene.name);
        }

        foreach (GameObject hunterObject in hunterObjects)
        {
            hunters.Add(hunterObject);
        }
    }
}
