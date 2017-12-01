﻿// Author: Peter Jæger
// Contributors: Emil
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;


public class DeerScenerio : MonoBehaviour 
{
	private EventDelegate hidden;
	private EventDelegate groundEvent;
	private EventDelegate  kills;

	public GameObject deer;
	public GameObject deadDeer;

	Animator anim;
	Rigidbody rb;
	float currentTime;
	int reactHash = Animator.StringToHash("deerReact");
	private bool found; 
	private bool run;

	bool kill;

	public Transform targetPoint;
	[SerializeField]
 	float accuracy = 10.0f;
	[SerializeField]
	private float turnRate = 10.0f;

	[SerializeField]
	private float runSpeed = 100.0f;

	Vector3 scarePoint;

    private bool inScenario;

    public float secondsBeforeRunAway = 3;
    private float timeElapsed = 0;

    private bool done;
    EventManager eventManager;

	void Start () {
        eventManager = EventManager.GetInstance();
        anim = GetComponentInChildren<Animator>();
		rb = GetComponent<Rigidbody>();

		hidden = HiddenTest;
		groundEvent = Scared;
		kills = Killed;
		eventManager.AddListener(CustomEvent.HiddenByFog, hidden);
		eventManager.AddListener(CustomEvent.SinkGround, groundEvent);
		eventManager.AddListener(CustomEvent.SpearHit, kills);
        eventManager.AddListener(CustomEvent.DeerScenarioEntered, SetInScenario);
    }


    // Update is called once per frame
    void Update () {
        if (run)
        {
            timeElapsed += Time.deltaTime;
        }

        if (timeElapsed > secondsBeforeRunAway && !done)
        {
            Vector3 relativePos = targetPoint.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            Vector3 v3Force = runSpeed * (targetPoint.position - transform.position).normalized;
            rb.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnRate);
            rb.rotation *= Quaternion.Euler(0, -90, 0);
            rb.AddForce(v3Force);
            anim.SetFloat("deerSpeed", 2);
            done = true;
        }
    }

    private IEnumerator RunAway()
    {
        yield return new WaitForSeconds(secondsBeforeRunAway);
        run = true;

    }

	public void HiddenTest(EventArgument argument)
	{
		if (argument.gameObjectComponent == this.gameObject)
		{
			found = !argument.boolComponent;
            if (found)
            {
                Scared(new EventArgument());
            }
		}
	}

    public void SetInScenario(EventArgument argument)
    {
        inScenario = true;
        eventManager.CallEvent(CustomEvent.BroadcastObjectLocation, new EventArgument
        {
            gameObjectComponent = gameObject
        });
    }

    public void Scared (EventArgument argument)
	{
		if (!inScenario || kill)
        {
            return;
        }
        anim.SetTrigger(reactHash);
        StartCoroutine(RunAway());
        eventManager.CallEvent(CustomEvent.ScenarioInteracted);
    }

	public void Killed (EventArgument argument)
	{
        kill = true;
    }

}
