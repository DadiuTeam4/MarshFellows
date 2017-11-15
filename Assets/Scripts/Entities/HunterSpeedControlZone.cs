// Author: Jonathan
// Contributers: More Jonathan

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HunterSpeedControlZone : MonoBehaviour
{
	public float speedZoneSpeed;
	private float speedAfterZone = 1.6f;
	private bool speedAfterZoneSat;

	private NavMeshAgent navMeshO;
	private NavMeshAgent navMeshP;
	
	void OnTriggerEnter(Collider other)
	{
		if (string.Compare(other.transform.name, "O") == 0 && !speedAfterZoneSat)
		{
			var navMeshList = FindObjectsOfType<Navigator>();
			foreach (Navigator nav in navMeshList)
			{
				if (string.Compare(nav.transform.name, "P") == 0)
				{
					navMeshO = other.gameObject.GetComponent<NavMeshAgent>();
					speedAfterZone = navMeshO.speed;
					navMeshO.speed = speedZoneSpeed;

					navMeshP = nav.gameObject.GetComponent<NavMeshAgent>();
					navMeshP.speed = speedZoneSpeed;
					speedAfterZoneSat = true;
				}
			}			
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (string.Compare(other.transform.name, "O") == 0)
		{
			navMeshO.speed = speedAfterZone;
			navMeshP.speed = speedAfterZone;
		}
	}
}
