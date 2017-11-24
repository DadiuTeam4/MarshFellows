//Author:Tilemachos
//Co-author:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class HunterSpeedReset : MonoBehaviour {

	public float speedAfterZone;
	private bool speedAfterZoneSat = false;
	private NavMeshAgent navMeshO;
	private NavMeshAgent navMeshP;


	void OnTriggerEnter(Collider other)
	{

		
		if (string.Compare(other.transform.name, "O") == 0 && !speedAfterZoneSat)
		{
			speedAfterZone = HunterSpeedControlZone.speedAfterZone;
		
			Debug.Log("SPEED AFTER : " + speedAfterZone);
			
			var navMeshList = FindObjectsOfType<Navigator>();
			foreach (Navigator nav in navMeshList)
			{
				if (string.Compare(nav.transform.name, "P") == 0)
				{
					navMeshO = other.gameObject.GetComponent<NavMeshAgent>();
					navMeshO.speed = speedAfterZone;

					navMeshP = nav.gameObject.GetComponent<NavMeshAgent>();
					navMeshP.speed = speedAfterZone;
					speedAfterZoneSat = true;
				}
			}			
		}

	}
}
