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
        public int firstIndexOfNextPossibleScene;
        public int secondIndexOfNextPossibleScene;

    }

    // Variables to keep track of scenes to load and unload.
    CustomScene previousScene;
    CustomScene currentScene;
    CustomScene nextScene;

    // Clusters of scenes to be loaded at certain points.
    string[] gameStart = {"GameOpener", "GlobalScene", "IntroLevel", "CrossRoad1" };
    string[] gameEnd = { "EndScene", "Credits" };

    CustomScene[] allScenes= {new CustomScene{nameOfScene = "GameOpener",firstIndexOfNextPossibleScene = 0,secondIndexOfNextPossibleScene = 2 }};

    void Start()
    {
        SceneClusterLoader(gameStart);
        //EventManager.AddListener(CustomEvent.changeScene, sceneLoader(EventArgument));

        //previousScene = "GameOpener";
        //currentScene = "IntroLevel";
       // nextScenes.Add ("CrossRoad1");

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
    private void sceneLoader(CustomScene currentScene)
    {
        previousScene = currentScene;
        currentScene = nextScene;
        nextScene = currentScene;

        foreach(int indexOfNextScene in currentScene)
        {

        }
        Scene scene = SceneManager.GetSceneByName(upComingScene);
        if (!scene.isLoaded)
        {
            SceneManager.LoadScene(upComingScene, LoadSceneMode.Additive);
        }

        SceneManager.UnloadSceneAsync(previousScene);

    }

    // Takes a GameScene Enum and returns the scene name string.
    private string GameSceneToString(CustomScene scene)
    {
        return scene.nameOfScene;
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