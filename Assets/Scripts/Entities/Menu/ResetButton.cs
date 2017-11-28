// Author: Itai Yavin
// Contributors: Kristian 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Events;

public class ResetButton : MonoBehaviour
{
    public AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.GetInstance();
    }

    public void ResetScene()
    {
        EventManager.GetInstance().CallEvent(CustomEvent.ResetGame);
        audioManager.OnMenuClick();
        
        EventManager eventManager = EventManager.GetInstance(); 
    
        EventArgument argument = new EventArgument(); 
        
        argument.stringComponent = "restart";
        argument.intComponent = 1;
        eventManager.CallEvent(CustomEvent.LoadScene,argument);
    }
}
