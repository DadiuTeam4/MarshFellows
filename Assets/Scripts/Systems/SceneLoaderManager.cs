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


    class CustomScene{
        public string nameOfScene;
        public string firstPossibleNextScene = "";
        public string secondPossibleNextScene = "";

    }

    private string notSetStringName = "";
    // Variables to keep track of scenes to load and unload.
    CustomScene previousScene;
    CustomScene prePreviousScene;
    
    CustomScene currentScene;

    // Clusters of scenes to be loaded at certain points.
    string[] gameStart = {"GameOpener", "GlobalScene", "IntroLevel", "CrossRoad1" };
    string[] gameEnd = { "EndScene", "Credits" };

    string globalSceneName = "GlobalScene";
    CustomScene[] allScenes= {  new CustomScene{nameOfScene = "GameOpener",firstPossibleNextScene = "GlobalScene",secondPossibleNextScene = "" },
                                new CustomScene{nameOfScene = "GlobalScene",firstPossibleNextScene = "IntroLevel",secondPossibleNextScene = "" },
                                new CustomScene{nameOfScene = "IntroLevel",firstPossibleNextScene = "Crossroads",secondPossibleNextScene = "" },
                                new CustomScene{nameOfScene = "Crossroads",firstPossibleNextScene = "EndScene",secondPossibleNextScene = "" }};

    void Start()
    {
        //SceneClusterLoader(gameStart);
        
        EventManager eventManager = EventManager.GetInstance();

    	EventDelegate sceneLoader = SceneLoader;

		eventManager.AddListener(CustomEvent.SceneHasChanged, sceneLoader);
        
        string currentSceneString = SceneManager.GetActiveScene().name;
        
        foreach(CustomScene sceneCheck in allScenes)
        {
            if(sceneCheck.nameOfScene == currentSceneString)
            {
                currentScene = sceneCheck;
                break;
            }
        }

        //EventManager.AddListener(CustomEvent.ChangeScene, sceneLoader(EventArgument));

        //previousScene = "GameOpener";
        //currentScene = "IntroLevel";
       // nextScenes.Add ("CrossRoad1");

    }

    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            EventManager eventManager = EventManager.GetInstance(); 
 
    	    EventArgument argument = new EventArgument(); 
            argument.stringComponent = "GlobalScene";
            eventManager.CallEvent(CustomEvent.SceneHasChanged,argument);
        }
    }

    // Start the game!
    private void SceneClusterLoader(string[] cluster)
    {
        foreach (string sceneName in cluster)
        {   
            Scene scene = SceneManager.GetSceneByName(sceneName);
            if (!scene.isLoaded)
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }
        }
    }

    // The main scene changing function. Updates scene trackers and loads and unloads scenes.
    private void SceneLoader(EventArgument argument)
    {
        prePreviousScene=previousScene;
        previousScene = currentScene;

        foreach(CustomScene sceneCheck in allScenes)
        {
            if(sceneCheck.nameOfScene == argument.stringComponent)
            {
                currentScene = sceneCheck;
                break;
            }
        }

        if(currentScene.firstPossibleNextScene != notSetStringName)
        {
            Scene scene = SceneManager.GetSceneByName(currentScene.firstPossibleNextScene);
            if (!scene.isLoaded)
            {
                SceneManager.LoadScene(currentScene.firstPossibleNextScene, LoadSceneMode.Additive);
            }
        }

        if(currentScene.secondPossibleNextScene != notSetStringName)
        {
            Scene scene = SceneManager.GetSceneByName(currentScene.secondPossibleNextScene);
            if (!scene.isLoaded)
            {
                SceneManager.LoadScene(currentScene.secondPossibleNextScene, LoadSceneMode.Additive);
            }
        }

        if(previousScene != null && prePreviousScene.nameOfScene != globalSceneName)
        {
            Scene unloadScene = SceneManager.GetSceneByName(prePreviousScene.nameOfScene);
            if (unloadScene.isLoaded)
                SceneManager.UnloadSceneAsync(prePreviousScene.nameOfScene);
        }
 

    }


/*
    public enum GameScene
    {
        GameOpener,
        GlobalScene,
        EndScene,
        Credits,
        IntroLevel,
        CrossRoad1,
        RitualEvent
    }
 */
}