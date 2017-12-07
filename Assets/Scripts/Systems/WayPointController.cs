//Author: Emil Villumsen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class WayPointController : MonoBehaviour {


    public Navigator O;
    public Transform oSeparationSplit;
    public Transform oBearSplit;
    public Transform oDeerSplit;
    public Transform oDeerKilledWaypoint;

    public Navigator P;
    public Transform pSeparationSplit;
    public Transform pSeparationSplit2;
    public Transform pBearSplit;
    public Transform pDeerSplit;
    public Transform pDeerKilledWaypoint;

    private EventManager eventManager;
    private EventDelegate separationScenario;
    private EventDelegate separationScenario2;
    private EventDelegate bearScenario;
    private EventDelegate deerScenario;


    private void Start()
    {
        eventManager = EventManager.GetInstance();
        separationScenario += SetSeparationWaypoints;
        separationScenario2 = SetSeperation2;
        bearScenario += SetBearWaypoints;
        deerScenario += SetDeerWaypoints;
        eventManager.AddListener(CustomEvent.SeparationScenarioEntered, separationScenario);
        eventManager.AddListener(CustomEvent.BearScenarioEntered, bearScenario);
        eventManager.AddListener(CustomEvent.DeerScenarioEntered, deerScenario);
        eventManager.AddListener(CustomEvent.LocationReached, separationScenario2);

    }

    void SetSeperation2(EventArgument args)
    {
        P.SetSplitWaypoint(pSeparationSplit2);
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

    void SetDeerWaypoints(EventArgument args)
    {
        O.SetSplitWaypoint(oDeerSplit);
        P.SetSplitWaypoint(pDeerSplit);
        eventManager.AddListener(CustomEvent.ThrowSpear, SetDeerDeadWaypoint);
        eventManager.RemoveListener(CustomEvent.LocationReached, separationScenario2);
    }

    void SetDeerDeadWaypoint(EventArgument args)
    {
        O.SetSplitWaypoint(oDeerKilledWaypoint);
        P.SetSplitWaypoint(pDeerKilledWaypoint);
        eventManager.RemoveListener(CustomEvent.LocationReached, separationScenario2);
    }
}
