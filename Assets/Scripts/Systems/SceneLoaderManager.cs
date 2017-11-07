//Author: Emil Villumsen
//Collaborator: Jonathan,Tilemachos
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Events;

//2 events - 1 that signals what scenes to load and one that signals my current scene

public class SceneLoaderManager : Singleton<SceneLoaderManager> 
{

    private string emptyString = "";
    // Variables to keep track of scenes to load and unload.
    List<string> scenesToUnload;

    // Clusters of scenes to be loaded at certain points.
   // string[] gameStart = {"GameOpener", "GlobalScene", "IntroLevel", "CrossRoad1" };
   // string[] gameEnd = { "EndScene", "Credits" };

    public string globalSceneName = "GlobalScene";

    void Start()
    {
        //SceneClusterLoader(gameStart);
        scenesToUnload = new List<string>();
        
        EventManager eventManager = EventManager.GetInstance();

    	EventDelegate sceneLoader = SceneLoader;

		eventManager.AddListener(CustomEvent.LoadScene, sceneLoader);


        EventArgument argument = new EventArgument(); 
        argument.stringComponent = "GlobalScene";
        argument.intComponent = 0;
        eventManager.CallEvent(CustomEvent.LoadScene,argument);

        argument.stringComponent = "IntroLevel";
        argument.intComponent = 1;
        eventManager.CallEvent(CustomEvent.LoadScene,argument);
        

    }

    // The main scene changing function. Updates scene trackers and loads and unloads scenes.
    private void SceneLoader(EventArgument argument)
    {
        if(argument.intComponent == 0)
        {
            scenesToUnload.Add(argument.stringComponent);
            return;
        }
        
        //if you sent a new scene to load
        Scene scene = SceneManager.GetSceneByName(argument.stringComponent);
        if (!scene.isLoaded)
        {
            SceneManager.LoadScene(argument.stringComponent, LoadSceneMode.Additive);
        }

        foreach(string sceneToUnload in scenesToUnload)
        {
            if(sceneToUnload != emptyString && sceneToUnload != globalSceneName)
            {
                Scene unloadScene = SceneManager.GetSceneByName(sceneToUnload);
                if (unloadScene.isLoaded)
                {
                    SceneManager.UnloadSceneAsync(sceneToUnload);
                }
            }
        }

    }

    
    // Start the game!
    /*private void SceneClusterLoader(string[] cluster)
    {
        foreach (string sceneName in cluster)
        {   
            Scene scene = SceneManager.GetSceneByName(sceneName);
            if (!scene.isLoaded)
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }
        }
    }*/

}