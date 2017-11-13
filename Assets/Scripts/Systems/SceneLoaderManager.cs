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
    EventManager eventManager;
    public string globalSceneName = "GlobalScene";
    public string firstSceneToLoadName = "IntroLevel";
    public string whoToAddTheUnlockables = "O";
    void Start()
    {
        //SceneClusterLoader(gameStart);
        scenesToUnload = new List<string>();
        
        eventManager = EventManager.GetInstance();

    	EventDelegate sceneLoader = SceneLoader;

		eventManager.AddListener(CustomEvent.LoadScene, sceneLoader);


        EventArgument argument = new EventArgument(); 
        argument.stringComponent = globalSceneName;
        argument.intComponent = 0;
        eventManager.CallEvent(CustomEvent.LoadScene,argument);

        argument.stringComponent = firstSceneToLoadName;
        argument.intComponent = 1;
        eventManager.CallEvent(CustomEvent.LoadScene,argument);


        AddUnlockables(whoToAddTheUnlockables);

        
    }
    private void AddUnlockables(string whoToAdd)
    {
        GameObject o = GameObject.Find(whoToAdd);
        if(GameStateManager.current != null)
        {
            for(int i = 0; i<GameStateManager.current.roundsPlayed;i++)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.AddComponent<MeshRenderer>();
                cube.transform.parent=o.transform;
                
                cube.transform.position = new Vector3(cube.transform.parent.position.x,cube.transform.parent.position.y+i,cube.transform.parent.position.z-1);
            }
        }
    }

    // The main scene changing function. Updates scene trackers and loads and unloads scenes.
    private void SceneLoader(EventArgument argument)
    {
        if(argument.intComponent == 0)
        {
            scenesToUnload.Add(argument.stringComponent);
            return;
        }
        if(argument.intComponent > 0)
        {
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
        if(argument.stringComponent == "restart" || argument.stringComponent == "Restart")
        {
            GameStateManager newRound = new GameStateManager();

            if(GameStateManager.current != null)
            {
			    newRound = GameStateManager.current;
            }

			newRound.playedBefore = true;
			newRound.roundsPlayed++;
			GameStateManager.current = newRound;

            SaveLoadManager.Save();
            UnloadAllScenes();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        if(argument.intComponent < 0)
        {
            print("Name of the scene is:" + argument.stringComponent + " Time for new Music" + argument.intComponent);
        }
    }

    void UnloadAllScenes() 
    {
     int c = SceneManager.sceneCount;
     for (int i = 0; i < c; i++) {
        Scene scene = SceneManager.GetSceneAt (i);       
        SceneManager.UnloadSceneAsync (scene);
    }
 }

}