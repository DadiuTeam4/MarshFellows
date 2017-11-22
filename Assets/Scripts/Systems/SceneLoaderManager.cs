//Author: Emil Villumsen
//Collaborator: Jonathan,Tilemachos
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Events;

public class SceneLoaderManager : Singleton<SceneLoaderManager> 
{

    private string emptyString = "";
    // Variables to keep track of scenes to load and unload.
    List<string> scenesToUnload;
    
    EventManager eventManager;
    public string globalSceneName = "GlobalScene";  
    public string initialSceneName = "IntroLevel";
    public string cutsceneName = "IntroCutScene";
    public Vector3 respawnPosition;
    public string PsName = "P";
    public string OsName = "O";
    public string cameraAndPOname = "OPandCamera";
    public GameObject fogOnRestart;
    public int offsetForCreatingFog = -55;
    void Start()
    {
        
        scenesToUnload = new List<string>();
        
        eventManager = EventManager.GetInstance();

    	EventDelegate sceneLoader = SceneLoader;

		eventManager.AddListener(CustomEvent.LoadScene, sceneLoader);


        EventArgument argument = new EventArgument(); 

        //load different level if it is a replay
        if(GameStateManager.current != null && GameStateManager.current.playedBefore)
        {
            argument.stringComponent =  initialSceneName;
            GameObject cpo = GameObject.Find(cameraAndPOname);// I really wanted to name it C-3PO
            if(respawnPosition != null && respawnPosition.magnitude != 0.0f && cpo != null)
            {
                cpo.transform.position = respawnPosition;
                Debug.Log("move");
            }
            if(fogOnRestart != null && cpo != null)
            {
                Instantiate(fogOnRestart, new Vector3(cpo.transform.position.x,cpo.transform.position.y,cpo.transform.position.z+offsetForCreatingFog), cpo.transform.rotation);
            }  
        }
        else
        {
            argument.stringComponent = initialSceneName;

        }

        //loading first scene
        argument.intComponent = 1;
        eventManager.CallEvent(CustomEvent.LoadScene,argument);
        Debug.Log(argument.stringComponent);
        AddUnlockables();
        
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
            Scene unloadScene = SceneManager.GetSceneByName(argument.stringComponent);
            if (unloadScene.isLoaded)
            {
                SceneManager.UnloadSceneAsync(argument.stringComponent);
            }            
            return;
        }

        if(argument.stringComponent == "restart" || argument.stringComponent == "Restart")
        {
            GameStateManager newRound = new GameStateManager();
            ChangeSceneEmitter.sceneIndex = 1;
            if(GameStateManager.current != null)
            {
			    newRound = GameStateManager.current;
            }

			newRound.playedBefore = true;
			newRound.roundsPlayed++;
			GameStateManager.current = newRound;

            SaveLoadManager.Save();
            UnloadAllScenes("");
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
            return;
        }

        if(argument.intComponent > 0)
        {
            //if you sent a new scene to load
            Scene scene = SceneManager.GetSceneByName(argument.stringComponent);
            if (!scene.isLoaded)
            {
                SceneManager.LoadSceneAsync(argument.stringComponent, LoadSceneMode.Additive);
            }

        }
    }

    void UnloadAllScenes(string unloadGlobal) 
    {
        int c = SceneManager.sceneCount;
        for (int i = 0; i < c; i++) 
        {
            Scene scene = SceneManager.GetSceneAt (i);  
            if(scene.name != unloadGlobal)
            {
                SceneManager.UnloadSceneAsync (scene);
            }     
        }
    }

}