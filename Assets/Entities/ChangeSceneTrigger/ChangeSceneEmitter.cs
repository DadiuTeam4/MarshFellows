//Author:Tilemachos
//Co-author:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

//0 for sending current scene , 1 for sending next in order to load, 2 for sending another one
public class ChangeSceneEmitter : MonoBehaviour {

    public GameObject blocker;
    public int offsetForCreatingObstacle = 10;
    public string currentScene = "";
    public string basicSceneToLoad="";
    public string secondarySceneToLoad="";
    private string emptyString = "";
    Collider m_ObjectCollider;

    void OnTriggerEnter(Collider other)
    {
        EventManager eventManager = EventManager.GetInstance(); 
 
    	EventArgument argument = new EventArgument(); 

        if(currentScene != emptyString)
        {
            argument.stringComponent = currentScene;
            argument.intComponent = 0;
            eventManager.CallEvent(CustomEvent.LoadScene,argument);
        }

        if(basicSceneToLoad != emptyString)
        {
            argument.stringComponent = basicSceneToLoad;
            argument.intComponent = 1;
            eventManager.CallEvent(CustomEvent.LoadScene,argument);
        }

        if(secondarySceneToLoad != emptyString)
        {
            argument.stringComponent = secondarySceneToLoad;
            argument.intComponent = 2;           
            eventManager.CallEvent(CustomEvent.LoadScene,argument);
        }

        Instantiate(blocker, transform.position + (transform.forward*-offsetForCreatingObstacle), this.gameObject.transform.rotation);
        Destroy(this.gameObject);


    }

}
