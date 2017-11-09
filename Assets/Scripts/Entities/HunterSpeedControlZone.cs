// Author: Jonathan
// Contributers:

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterSpeedControlZone : MonoBehaviour
{
	public float speedZoneSpeed;
	public float speedAfterZone = 1.6f;
	
	void OnTriggerEnter(Collider other)
	{
		if (string.Compare(other.transform.name, "O") == 0)
		{
			var navMesh = other.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
			navMesh.speed = speedZoneSpeed;			
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (string.Compare(other.transform.name, "O") == 0)
		{
			var navMesh = other.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
			navMesh.speed = speedAfterZone;			
		}
	}
}
