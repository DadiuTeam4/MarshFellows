//Author: Emil Villumsen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class Shaman : MonoBehaviour {

    private EventManager eventManager;
    private EventDelegate shamanDelegate;
    private EventDelegate locationDelegate;
    public CustomEvent eventToBroadcastOn;

    // Use this for initialization
    void Start () {
        eventManager = EventManager.GetInstance();
        shamanDelegate = Disrupted;
        locationDelegate = BroadCastLocation;
        eventManager.AddListener(CustomEvent.RitualDisrupted, shamanDelegate);
        eventManager.AddListener(eventToBroadcastOn, locationDelegate);
    }

    void BroadCastLocation(EventArgument args) {
        EventArgument outArgs = new EventArgument
        {
            gameObjectComponent = this.gameObject
        };
        eventManager.CallEvent(CustomEvent.BroadcastObjectLocation, outArgs);
	}

    void Disrupted(EventArgument args)
    {
        GetComponentInChildren<MeshRenderer>().material.color = Color.red;
    }
}
