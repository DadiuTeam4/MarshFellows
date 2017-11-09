//Author: Emil Villumsen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class WayPointController : MonoBehaviour {


    public Navigator O;
    public Transform oSeparationSplit;
    public Transform oBearSplit;


    public Navigator P;
    public Transform pSeparationSplit;
    public Transform pBearSplit;

    private EventManager eventManager;
    private EventDelegate separationScenario;
    private EventDelegate bearScenario;

    private void Start()
    {
        eventManager = EventManager.GetInstance();
        separationScenario += SetSeparationWaypoints;
        bearScenario += SetBearWaypoints;
        eventManager.AddListener(CustomEvent.SeparationScenarioEntered, separationScenario);
        eventManager.AddListener(CustomEvent.BearScenarioEntered, bearScenario);
        print("events added");
    }

    void SetBearWaypoints(EventArgument args) {
        O.SetSplitWaypoint(oBearSplit);
        P.SetSplitWaypoint(pBearSplit);
    }

    void SetSeparationWaypoints(EventArgument args)
    {
        O.SetSplitWaypoint(oSeparationSplit);
        P.SetSplitWaypoint(pSeparationSplit);
    }
}
