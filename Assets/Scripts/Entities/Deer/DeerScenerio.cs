﻿// Author: Peter Jæger
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;


public class DeerScenerio : MonoBehaviour 
{
	private EventDelegate hidden;
	private EventDelegate groundEvent;
	private EventDelegate  kills;
	private EventDelegate swipeEvent;

	public GameObject deer;
	public GameObject deadDeer;

	Animator anim;
	Rigidbody rb;
	float currentTime;
	int reactHash = Animator.StringToHash("deerReact");
	private bool found; 
	private bool run;

	bool kill;

	[SerializeField]
	private GameObject targetPoint;
	[SerializeField]
 	float accuracy = 10.0f;
	[SerializeField]
	private float turnRate = 10.0f;

	[SerializeField]
	private float runSpeed = 100.0f;

	Vector3 scarePoint;
	
	void Start () {
		anim = GetComponentInChildren<Animator>();
		rb = GetComponent<Rigidbody>();

		hidden = HiddenTest;
		groundEvent = Scared;
		kills = Killed;
		swipeEvent = Swipe;
		EventManager.GetInstance().AddListener(CustomEvent.HiddenByFog, hidden);
		EventManager.GetInstance().AddListener(CustomEvent.SinkGround, groundEvent);
		EventManager.GetInstance().AddListener(CustomEvent.DeerKilledEvent, kills);
		EventManager.GetInstance().AddListener(CustomEvent.Swipe, swipeEvent);
		
	}
	
	// Update is called once per frame
	void Update () {
		if(found && !kill && !run){
			print("deer found");
			anim.SetTrigger(reactHash);
		}

		if (run && !kill)
		{
			print("deer run");
			Vector3 relativePos = targetPoint.transform.position - transform.position;
			Quaternion rotation = Quaternion.LookRotation(relativePos);
			Vector3 v3Force = runSpeed * transform.forward;
			rb.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnRate);
			rb.AddForce(v3Force);
			anim.SetFloat("deerSpeed", rb.velocity.magnitude);
		}

		if(kill || Input.GetKeyDown(KeyCode.K))
		{
			kill = true;
			deadDeer.SetActive(true);
			deer.SetActive(false);
		}

	}

	public void HiddenTest(EventArgument argument)
	{
		if (argument.gameObjectComponent == this.gameObject)
		{
			found = !argument.boolComponent;
		}
	}

	public void Scared (EventArgument argument)
	{
		

		scarePoint = argument.vectorComponent;
		scarePoint = scarePoint + argument.gameObjectComponent.transform.position;
		float dist = (scarePoint - transform.position).magnitude;
		if (dist < accuracy)
		{
			run = true;
		}
	}

	public void Killed (EventArgument argument)
	{
		kill = true;
	}

	public void Swipe (EventArgument argument)
	{
		scarePoint = argument.raycastComponent.point;
		float dist = (scarePoint - transform.position).magnitude;
		if (dist < accuracy && found)
		{
			run = true;
		}
	}
}
