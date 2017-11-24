﻿// Author: Jonathan
// Contributers:Tilemachos

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HunterSpeedControlZone : MonoBehaviour
{
	public float speedZoneSpeed;
	static public float speedAfterZone;
	private bool speedAfterZoneSat;

	private NavMeshAgent navMeshO;
	private NavMeshAgent navMeshP;
	
	void OnTriggerEnter(Collider other)
	{
		
		if (string.Compare(other.transform.name, "O") == 0 && !speedAfterZoneSat)
		{
			speedAfterZone = GlobalConstantsManager.GetInstance().constants.speed;
			
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
}
