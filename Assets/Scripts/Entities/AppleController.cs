// Author: You Wu
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class AppleController : Shakeable
{

    private Rigidbody appleRd;
    private bool isGravityActived;

    public float thresholdForAppleFallDown = 800f;

    void Awake()
    {
        appleRd = GetComponent<Rigidbody>();
    }

    void Start()
    {
        isGravityActived = false;
    }

    public override void OnShakeBegin(float magnitude)
    {
        if (!isGravityActived)
        {
            CheckAppleFall(magnitude);
        }
    }

    public override void OnShake(float magnitude)
    {
        if (!isGravityActived)
        {
            CheckAppleFall(magnitude);
        }
        else
        {
            appleRd.AddForce(GetShakeForceOnShakebleObject(magnitude));
        }

    }

    private void CheckAppleFall(float magnitude)
    {
        if (magnitude > thresholdForAppleFallDown)
        {
            appleRd.useGravity = true;
            isGravityActived = true;
			EventArgument args = new EventArgument ();
			args.stringComponent = "AppleFall";
            EventManager.GetInstance().CallEvent(CustomEvent.AppleFall, args);
        }
    }

}
