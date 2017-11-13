// Author: Peter Jæger
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class DeerAnimationController : MonoBehaviour 
{
	private EventDelegate eventDelegate;
	// Use this for initialization
	Animator anim;
	Rigidbody rb;
	float currentTime;
	int reactHash = Animator.StringToHash("deerReact");
	private bool found; 
	private bool run;

	[SerializeField]
	private float runDelay = 3.0f;

	[SerializeField]
	private Vector3 targetPoint;

	[SerializeField]
	private float turnRate = 10.0f;

	[SerializeField]
	private float runSpeed = 100.0f;

	void Start () 
	{
		anim = GetComponentInChildren<Animator>();
		rb = GetComponent<Rigidbody>();

		eventDelegate = HiddenTest;
		EventManager.GetInstance().AddListener(CustomEvent.HiddenByFog, eventDelegate);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		if (found)
		{
			anim.SetTrigger(reactHash);
			currentTime = Time.time;
			found = false;
			run = true;
		}
		
		if ((currentTime + stateInfo.length + runDelay) < Time.time && run)
		{
			Vector3 relativePos = targetPoint - transform.position;
			Quaternion rotation = Quaternion.LookRotation(relativePos);
			Vector3 v3Force = runSpeed * transform.forward;
			rb.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnRate);
			rb.AddForce(v3Force);
			anim.SetFloat("deerSpeed", rb.velocity.magnitude);
		}
		
		if ((transform.position - targetPoint).magnitude < 10)
		{
			run = false;
			rb.velocity = new Vector3(0,0,0);
		}

	}

	public void HiddenTest(EventArgument argument)
	{
		if (argument.gameObjectComponent == this.gameObject)
		{
			found = !argument.boolComponent;
		}
	}




}
