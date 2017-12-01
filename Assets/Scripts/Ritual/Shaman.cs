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

    private Animator animator;

    public float transformAfterSec = 20;
    private float timeElapsed = 0;
    private bool disrupted = false; // Has the shaman been disrupted in his transformation?
    private bool inScenario = false; // To know when the hunter's are in the ritual scenario


    // Use this for initialization
    void Start () {
        animator = GetComponentInChildren<Animator>();
        eventManager = EventManager.GetInstance();
        shamanDelegate = Disrupted;
        locationDelegate = BroadCastLocation;
        eventManager.AddListener(CustomEvent.RitualDisrupted, shamanDelegate);
        eventManager.AddListener(CustomEvent.RitualScenarioEntered, SetInScenario);
        eventManager.AddListener(eventToBroadcastOn, BroadCastLocation);
    }

    private void Update()
    {
        if (!inScenario)
        {
            return;
        }

        timeElapsed += Time.deltaTime;
        if (!disrupted && timeElapsed > transformAfterSec)
        {
            eventManager.RemoveListener(CustomEvent.RitualDisrupted, shamanDelegate);
            animator.SetBool("shamanTransform", true);
        }
    }

    void SetInScenario(EventArgument args)
    {
        inScenario = true;
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
        disrupted = true;
        GetComponentInChildren<MeshRenderer>().material.color = Color.red;
    }
}
