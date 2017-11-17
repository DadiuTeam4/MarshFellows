// Author: You Wu
// Contributors: tilemachos
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class ObjectFall : MonoBehaviour
{
    private Quaternion initialRotation;
    private Vector3 objectSize;
    private bool hasFall;
    public bool checkRotation = true;
    public bool checkPosition = true;
    public string typeOfObject;

    void Start()
    {
        initialRotation = transform.rotation;
        hasFall = false;
        objectSize = GetComponent<Collider>().bounds.size;
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
        if (checkRotation)
        {
            CheckFallByRotation();
        }
        if (checkPosition)
        {
            CheckFallByPosition();
        }

    }

    private void CheckFallByRotation()
    {
        Quaternion currentRotation = transform.rotation;
        if (Mathf.Abs(currentRotation.eulerAngles.x - initialRotation.eulerAngles.x) > 180)
        {
            CallFallEvent();
        }
    }

    private void CheckFallByPosition()
    {
        Vector3 currentPosition = transform.position;
        if(transform.position.y < objectSize.y + 0.5)
        {
            Debug.Log("Fall");
            CallFallEvent();
        }
    }

    private void CallFallEvent()
    {
        hasFall = true;
        EventManager eventManager = EventManager.GetInstance();
        EventArgument argument = new EventArgument();
        argument.stringComponent = typeOfObject;
        eventManager.CallEvent(CustomEvent.FallHasHappend, argument);
        //Stop Update
        enabled = false;
    }

}
