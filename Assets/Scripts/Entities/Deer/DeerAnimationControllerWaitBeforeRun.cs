// Author: Peter Jæger
// Contributors: Jonathan

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class DeerAnimationControllerWaitBeforeRun : MonoBehaviour 
{
	private EventDelegate fogEvent;
	private EventDelegate scareEvent;
	// Use this for initialization
	Animator anim;
	Rigidbody rigidbody;
	float currentTime;
	int reactHash = Animator.StringToHash("deerReact");
	int RunHash = Animator.StringToHash("deerSpeed");

	[SerializeField]
	private bool found;
	private bool scared;

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
		rigidbody = GetComponent<Rigidbody>();

		fogEvent = HiddenTest;
		scareEvent = scaredTest;
		EventManager.GetInstance().AddListener(CustomEvent.HiddenByFog, fogEvent);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		if(found)
		{
			anim.SetTrigger(reactHash);
			currentTime = Time.time;
			found = false;
		}
		
		if((currentTime + stateInfo.length + runDelay) < Time.time)
		{
			print("run");
			Vector3 relativePos = targetPoint - transform.position;
			Quaternion rotation = Quaternion.LookRotation(relativePos);
			Vector3 v3Force = runSpeed * transform.forward;
			rigidbody.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnRate);
			rigidbody.AddForce(v3Force);
			anim.SetFloat("deerSpeed", rigidbody.velocity.magnitude);
		}
		
		if((transform.position - targetPoint).magnitude < 2)
		{
			Destroy(this.gameObject);
		}

	}

	public void HiddenTest(EventArgument argument)
	{
		found = true;
	}
	
	public void ScaredTest(EventArgument argument)

}
