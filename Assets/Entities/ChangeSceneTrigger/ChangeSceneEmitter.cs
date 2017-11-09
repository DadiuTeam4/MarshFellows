//Author:Tilemachos
//Co-author:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using UnityEngine.SceneManagement;

//0 for sending current scene , 1 for sending next in order to load, 2 for sending another one
public class ChangeSceneEmitter : MonoBehaviour {

    public GameObject blocker;
    public int offsetForCreatingObstacle = 10;
    public List<string> nextUnloads;
    public List<string> scenesToLoad;
    public string unlockableInThisScreen = "";
    private string emptyString = "";
    Collider m_ObjectCollider;
    GameStateManager gameState = new GameStateManager();
    static int sceneIndex = 1;
    private bool addOnSceneIndex= true;

    void OnTriggerEnter(Collider other)
    {
        EventManager eventManager = EventManager.GetInstance(); 
 
    	EventArgument argument = new EventArgument(); 

        foreach(string unloadSceneName in nextUnloads)
        {
            if(unloadSceneName != emptyString)
            {
                argument.stringComponent = unloadSceneName;
                argument.intComponent = 0;
                eventManager.CallEvent(CustomEvent.LoadScene,argument);
            }
        }
        int index = 0;
        foreach(string loadScene in scenesToLoad)
        {
            index++;
            if(loadScene != emptyString)
            {
                argument.stringComponent = loadScene;
                argument.intComponent = index;
                eventManager.CallEvent(CustomEvent.LoadScene,argument);
            }
        }
    
        if (addOnSceneIndex)
        {
            argument.stringComponent = SceneManager.GetSceneAt(sceneIndex).name;
            argument.intComponent = -1;
            eventManager.CallEvent(CustomEvent.LoadScene,argument);
            sceneIndex++;
            addOnSceneIndex = false;
        }

        if(unlockableInThisScreen != emptyString)
        {
            ///NOT WORKING PROPERLY NEEDS FIX
            //saves null instead of the list
            gameState = GameStateManager.current;
            if(GameStateManager.current == null)
            {
                gameState.unlockables = new List<string>();
            }
//            gameState.unlockables.Add(argument.stringComponent);
            GameStateManager.current = gameState;
        }

        Instantiate(blocker, transform.position + (transform.forward*-offsetForCreatingObstacle), this.gameObject.transform.rotation);
        Destroy(this.gameObject);
    }

}
