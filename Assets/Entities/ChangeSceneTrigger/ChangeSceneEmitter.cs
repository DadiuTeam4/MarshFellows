//Author:Tilemachos
//Co-author:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;


public class ChangeSceneEmitter : MonoBehaviour {

    public string currentScene;

    void OnTriggerEnter(Collider other)
    {
        EventManager eventManager = EventManager.GetInstance(); 
 
    	EventArgument argument = new EventArgument(); 
        argument.stringComponent = currentScene;
        eventManager.CallEvent(CustomEvent.SceneHasChanged,argument);
    }

}
