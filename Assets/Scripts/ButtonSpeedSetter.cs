//Author:Tilemachos
//Co-author:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ButtonSpeedSetter : MonoBehaviour {

	[SerializeField]
	private List<NavMeshAgent> hunters = new List<NavMeshAgent>();

	public float speedToSet = 1.6f;
	void Start()
	{
		GameObject[] hunterGameObjects = new GameObject[2];
		hunterGameObjects[0] = GameObject.FindGameObjectWithTag("P");
		hunterGameObjects[1] = GameObject.FindGameObjectWithTag("O");
		
		for (int i = 0; i < hunterGameObjects.Length; i++)
		{
			NavMeshAgent agent = hunterGameObjects[i].GetComponent<NavMeshAgent>();
			
			if (agent)
			{
				hunters.Add(agent);
			}
		}
	}

	public void SetSpeed() 
	{
		foreach (NavMeshAgent agent in hunters)
		{
			agent.speed = speedToSet;
		}
	}
}