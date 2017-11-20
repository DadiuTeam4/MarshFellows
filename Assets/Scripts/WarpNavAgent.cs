//Author: Emil Villumsen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WarpNavAgent : MonoBehaviour {

    public Transform warpWaypoint;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag != "ScenarioTrigger")
        {
            return;
        }

        if (!warpWaypoint)
        {
            Debug.LogError("WarpNavAgent: No waypoint assigned to warp to");
            return;
        }
        NavMeshAgent navAgent = other.gameObject.GetComponentInParent<NavMeshAgent>();
        if (!navAgent)
        {
            Debug.LogError("WarpNavAgent: Couldn't find NavMeshAgent component, consider using another GetComponent function to locate it");
            return;
        }

        if (!navAgent.Warp(warpWaypoint.position)) 
        {
            Debug.LogError("Warp failed, is the warp location properly placed on a NavMesh?");
        }

    }
}
