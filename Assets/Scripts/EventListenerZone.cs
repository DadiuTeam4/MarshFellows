//Author: Emil Villumsen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class EventListenerZone : MonoBehaviour {


    public bool huntersInZone = false;
    public bool currentlyHidden = true;


    private EventManager eventManager;
    private EventDelegate eventDelegate;

    // Use this for initialization
    void Start () {
        eventManager = EventManager.GetInstance();
        eventDelegate = FogReveal;
        eventManager.AddListener(CustomEvent.HiddenByFog, eventDelegate);
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "ScenarioTrigger")
        {
            return;
        }
        huntersInZone = true;
        if (huntersInZone && !currentlyHidden)
        {

            eventManager.CallEvent(CustomEvent.ScenarioInteracted);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag != "ScenarioTrigger")
        {
            return;
        }

        huntersInZone = false;
    }

    private void FogReveal(EventArgument args)
    {
        if (args.stringComponent == "shaman")
        {
            currentlyHidden = args.boolComponent;
        }
        if (huntersInZone && !currentlyHidden) {
            EventArgument callArgs = new EventArgument
            {
                stringComponent = "Ritual"
            };
            eventManager.CallEvent(CustomEvent.ScenarioInteracted, callArgs);
        }
    }
}
