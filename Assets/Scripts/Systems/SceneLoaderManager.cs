//Author: Emil Villumsen
//Collaborator: Jonathan,Tilemachos
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Events;

public class SceneLoaderManager : Singleton<SceneLoaderManager> 
{

    // Variables to keep track of scenes to load and unload.
    List<string> scenesToUnload;
    
    EventManager eventManager;
    public string globalSceneName = "GlobalScene";  
    public string audioSceneName = "AudioScene";
    public string initialSceneName = "IntroLevel";
    public string PsName = "P";
    public string OsName = "O";
    public string cameraAndPOname = "OPandCamera";
    void Awake()
    {
        LoadInitialLevels();
    }
    void Start()
    {
        AddListenerForLoadingScene();        

        AddUnlockables();
        
    }

    private void AddListenerForLoadingScene()
    {
        eventManager = EventManager.GetInstance();

    	EventDelegate sceneLoader = SceneLoader;

		eventManager.AddListener(CustomEvent.LoadScene, sceneLoader);
    }

    private void LoadInitialLevels()
    {
        SyncLoadOfScenes(initialSceneName);

        SyncLoadOfScenes(audioSceneName);
        
    }
    private void AddUnlockables()
    {
        GameObject p = GameObject.Find(PsName);
        if(GameStateManager.current != null && GameStateManager.current.forPUnlockables != null)
        {
            for(int i = 0; i < GameStateManager.current.forPUnlockables.Count; i++)
            {
               try
               {
                   //example
                   //hunter_fqi02/Hunter01/global01/bn_root/bn_pelvis/Flint_axe
                    GameObject objectUnlocked = p.transform.Find(GameStateManager.current.forPUnlockables[i]).gameObject;
                    objectUnlocked.SetActive(true);
               }
               catch
               {
                   Debug.Log("For P Wrong name path for unlockable " + GameStateManager.current.forPUnlockables[i]+"|");
               }
            }
        }


        GameObject o = GameObject.Find(OsName);
        if(GameStateManager.current != null && GameStateManager.current.forOUnlockables != null)
        {
            for(int i = 0; i<GameStateManager.current.forOUnlockables.Count; i++)
            {
                try
               {
                    GameObject objectUnlocked = o.transform.Find(GameStateManager.current.forOUnlockables[i]).gameObject;
                    objectUnlocked.SetActive(true); 
               }
               catch
               {
                   Debug.Log("For O Wrong name path for unlockable " + GameStateManager.current.forOUnlockables[i]+"|");
               }
           
            }
        }
    }

    // The main scene changing function. Updates scene trackers and loads and unloads scenes.
    private void SceneLoader(EventArgument argument)
    {
        if(argument.intComponent == 0)
        {
            AsyncLoadOfScenes(argument.stringComponent);
            return;
        }

        if(argument.stringComponent == "restart" || argument.stringComponent == "Restart")
        {
            OnRestart();
            return;
        }

        if(argument.intComponent == 100)
        {
            SyncLoadOfScenes(argument.stringComponent);
            return;
        }

        if(argument.intComponent > 0)
        {
            //if you sent a new scene to load
            AsyncLoadOfScenes(argument.stringComponent);

        }
    }

    void SyncLoadOfScenes(string nameOfScene)
    {
        Scene scene = SceneManager.GetSceneByName(nameOfScene);
        if (!scene.isLoaded)
        {
            SceneManager.LoadScene(nameOfScene, LoadSceneMode.Additive);
        }
    }

    void AsyncLoadOfScenes(string nameOfScene)
    {
        StartCoroutine(LoadSceneAsync(nameOfScene));
    }

    IEnumerator LoadSceneAsync(string nameOfScene)
    {
        Scene scene = SceneManager.GetSceneByName(nameOfScene);
        if (!scene.isLoaded)
        {
            SceneManager.LoadSceneAsync(nameOfScene, LoadSceneMode.Additive);
        }
        yield return null;
    }

    void AsyncUnloadOfScenes(string nameOfScene)
    {
        Scene unloadScene = SceneManager.GetSceneByName(nameOfScene);
        if (unloadScene.isLoaded)
        {
            SceneManager.UnloadSceneAsync(nameOfScene);
        }       
    }

    private void OnRestart()
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
        eventManager.CallEvent(CustomEvent.ResetGame);
        SceneManager.LoadScene(globalSceneName);
    }
    
    void UnloadAllScenes() 
    {
        
        int c = SceneManager.sceneCount;
        //Unload last scene is not supported
        for (int i = 0; i < c - 1; i++) 
        {
            Scene scene = SceneManager.GetSceneAt (i);  
            SceneManager.UnloadSceneAsync (scene);
        }
    }

}