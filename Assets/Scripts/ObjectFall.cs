// Author: You Wu
// Contributors: tilemachos, Itai Yavin
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

[RequireComponent(typeof(Rigidbody))]
public class ObjectFall : ObjectType
{
    [SerializeField]
    private Type typeOfObject;
    
    [Tooltip("Minimum angular speed needed before the object is counted as falling")]
    public float minimumAngularSpeed = 1.0f;
    [Tooltip("Minimum speed needed before the object is counted as falling")]
    public float minimumSpeed = 1.0f;

    private float previousAngularSpeed;
    private float previousSpeed;
    
    private bool isFalling = false;
    private bool hasFallen = false;

    private new Rigidbody rigidbody;

    EventManager eventManager;
    EventArgument argument = new EventArgument();

    void Start()
    {   
        eventManager = EventManager.GetInstance();

        SetupStringValues();

        rigidbody = GetComponent<Rigidbody>();

        argument.stringComponent = GetTypeStringValue(typeOfObject);
        argument.gameObjectComponent = gameObject;
    }

    void Update()
    {
        CheckIfFalling();
    }

    private void CheckIfFalling()
    {
        float currentAngularSpeed = rigidbody.angularVelocity.magnitude;
        float currentSpeed = rigidbody.velocity.magnitude;

        if (isFalling && currentAngularSpeed <= minimumAngularSpeed && currentSpeed <= minimumSpeed)
        {
            isFalling = false;
            hasFallen = true;
            eventManager.CallEvent(CustomEvent.FallHasHappend, argument);
            
            return;
        }

        previousAngularSpeed = currentAngularSpeed;
        previousSpeed = currentSpeed;

        if (!isFalling && (currentAngularSpeed > minimumAngularSpeed || currentSpeed > minimumSpeed))
        {
            isFalling = true;
            hasFallen = false;
            return;
        }
    }
}
