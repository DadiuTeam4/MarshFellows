﻿//Author:Tilemachos
//Co-author:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using UnityEngine.SceneManagement;

public class ChangeSceneEmitter : MonoBehaviour {

    public GameObject blocker;
    public int offsetForCreatingObstacle = 10;
    public List<string> nextUnloads;
    public List<string> scenesToLoad;
    public string forPUnlockableInThisScene = "";
    public string forOUnlockableInThisScene = "";
    
    private string emptyString = "";
    Collider m_ObjectCollider;
    private bool haveBeenTriggered = false;

    void OnTriggerEnter(Collider other)
    {

        if(!haveBeenTriggered && (other.gameObject.tag == "O" || other.gameObject.tag == "P" || other.gameObject.tag == "ScenarioTrigger"))
        {

            
            GameStateManager gameState = new GameStateManager();
            
            if(forPUnlockableInThisScene != emptyString)
            {
                
                if(GameStateManager.current != null)
                {
                    gameState = GameStateManager.current;
                }
                if(gameState.forPUnlockables == null)
                {
                    gameState.forPUnlockables = new List<string>();
                }
                if(gameState != null && gameState.forPUnlockables != null && !gameState.forPUnlockables.Contains(forPUnlockableInThisScene))
                {
                    gameState.forPUnlockables.Add(forPUnlockableInThisScene);
                    GameStateManager.current = gameState;
                }
            }

            if(forOUnlockableInThisScene != emptyString)
            {
                
                if(GameStateManager.current != null)
                {
                    gameState = GameStateManager.current;
                }
                if(gameState.forOUnlockables == null)
                {
                    gameState.forOUnlockables = new List<string>();
                }
                
                if(gameState != null && gameState.forOUnlockables != null && !gameState.forOUnlockables.Contains(forOUnlockableInThisScene))
                {
                    gameState.forOUnlockables.Add(forOUnlockableInThisScene);
                    GameStateManager.current = gameState;
                }
            }

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

            //Calling current scene, needed for audio. TODO: Should be in a sepate script imo.
            EventArgument currentSceneArg = new EventArgument
            {
                stringComponent = gameObject.scene.name
            };
            eventManager.CallEvent(CustomEvent.CurrentScene, currentSceneArg);
            
            // Instantiate(blocker, transform.position + (transform.forward*-offsetForCreatingObstacle), this.gameObject.transform.rotation);
            Destroy(this.gameObject);

            haveBeenTriggered = true;
        }
        
    }

}
