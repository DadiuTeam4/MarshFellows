//Author: Emil Villumsen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class EventListenerZone : MonoBehaviour {


    private bool huntersInZone = false;
    private bool currentlyHidden = true;


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
        huntersInZone = true;
        if (huntersInZone && !currentlyHidden)
        {
            eventManager.CallEvent(CustomEvent.ScenarioInteracted);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        huntersInZone = false;
    }

    private void FogReveal(EventArgument args)
    {
        currentlyHidden = args.boolComponent;
        print(currentlyHidden);
        if (huntersInZone && !currentlyHidden)
        {
            eventManager.CallEvent(CustomEvent.ScenarioInteracted);
        }
    }
}
