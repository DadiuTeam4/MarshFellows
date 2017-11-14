//Author: Emil Villumsen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class Shaman : MonoBehaviour {

    private EventManager eventManager;
    private EventDelegate shamanDelegate;


    // Use this for initialization
    void Start () {
        eventManager = EventManager.GetInstance();
        shamanDelegate = Disrupted;
        eventManager.AddListener(CustomEvent.RitualDisrupted, shamanDelegate);
    }

    // Update is called once per frame
    void Update () {
		
	}

    void Disrupted(EventArgument args)
    {
        GetComponentInChildren<MeshRenderer>().material.color = Color.red;
    }
}
