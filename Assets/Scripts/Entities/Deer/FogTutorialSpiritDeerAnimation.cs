// Author: Peter Jæger
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class FogTutorialSpiritDeerAnimation : MonoBehaviour 
{
	private EventDelegate fogEvent;
	private EventDelegate scareEvent;
	private EventDelegate groundEvent;

	private EventDelegate swipeEvent;
	Animator anim;
	Rigidbody rigidbody;
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
	
	Vector3 scarePoint;

	[SerializeField]
	float accuracy = 10.0f;

	bool triggered = false;

	void Awake () 
	{
		anim = GetComponentInChildren<Animator>();
		rigidbody = GetComponent<Rigidbody>();

		fogEvent = HiddenTest;
		scareEvent = Scared;
		groundEvent = Scared;
		swipeEvent = Swipe;
		EventManager.GetInstance().AddListener(CustomEvent.HiddenByFog, fogEvent);
		EventManager.GetInstance().AddListener(CustomEvent.ScareDeerEvent, scareEvent);
		EventManager.GetInstance().AddListener(CustomEvent.SinkGround, groundEvent);
		EventManager.GetInstance().AddListener(CustomEvent.Swipe, swipeEvent);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		if(found)
		{
			anim.SetTrigger(reactHash);
		}
		
		if(run && found)
		{
			Vector3 relativePos = targetPoint - transform.position;
			Quaternion rotation = Quaternion.LookRotation(relativePos);
			Vector3 v3Force = runSpeed * transform.forward;
			rigidbody.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnRate);
			rigidbody.AddForce(v3Force);
			anim.SetFloat("deerSpeed", rigidbody.velocity.magnitude);
			if(!triggered)
			{
			EventManager.GetInstance().CallEvent(CustomEvent.ScenarioInteracted);
			triggered = true;
			}
		}
		
		if ((transform.position - targetPoint).magnitude < 10)
		{
			run = false;
		}

		if (Input.GetKey(KeyCode.S) && found)
		run = true;

	}

	public void HiddenTest(EventArgument argument)
	{
		if (argument.gameObjectComponent == this.gameObject)
		{
			found = !argument.boolComponent;
		}
	}

	public void Scared(EventArgument argument)
	{
		scarePoint = argument.vectorComponent;
		scarePoint = scarePoint + argument.gameObjectComponent.transform.position;
		float dist = (scarePoint - transform.position).magnitude;
		if (dist < accuracy && found)
		{
			run = true;
		}
	}

		public void Swipe(EventArgument argument)
	{
		scarePoint = argument.raycastComponent.point;
		float dist = (scarePoint - transform.position).magnitude;
		if (dist < accuracy && found)
		{
			run = true;
		}
	}



}
