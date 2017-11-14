// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HunterSpeedController : MonoBehaviour 
{
	[SerializeField]
	private NavMeshAgent[] hunters;

	void Start()
	{

	}

	public void SetSpeed(float speed) 
	{
		foreach (NavMeshAgent agent in hunters)
		{
			agent.speed = speed;
		}
	}
}
