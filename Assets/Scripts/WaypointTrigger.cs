// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointTrigger : MonoBehaviour 
{
	[SerializeField]
	private Transform nextDestination;

	[Tooltip("The tag that the script is looking for. Is by default O")]
	public string tag = "O";
	
	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag != tag)
		{
			return;
		}
	
		Navigator agent = other.GetComponent<Navigator>(); 
	
		if (agent)
		{
			agent.waypoint = nextDestination;
			agent.SetDestination();
		}
	}
}
