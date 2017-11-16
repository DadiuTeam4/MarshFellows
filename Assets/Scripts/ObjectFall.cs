// Author: You Wu
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class ObjectFall : MonoBehaviour
{
    private Quaternion initialRotation;
    private bool hasFall;

    void Start()
    {
        initialRotation = transform.rotation;
        hasFall = false;
    }

    void Update()
    {
        if (!hasFall)
        {
            CheckIfFall();
        }

    }

    private void CheckIfFall()
    {
        Quaternion currentRotation = transform.rotation;
        if (Mathf.Abs(currentRotation.eulerAngles.x - initialRotation.eulerAngles.x) > 180)
        {
            hasFall = true;
            EventManager eventManager = EventManager.GetInstance();

            EventArgument argument = new EventArgument();

            eventManager.CallEvent(CustomEvent.FallHasHappend, argument);
        }
    }
}
