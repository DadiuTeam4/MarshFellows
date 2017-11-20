//Author:Tilemachos
//Co-author: You Wu
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class SinkableObjectType : MonoBehaviour
{

    private Vector3 initialPosition;
    public string typeOfSinkable = "tree";
    private bool hasSunk;
    public Transform groundTransform;


    void Start()
    {
        initialPosition = transform.position;
        InitStatusOfSink();
    }

    private void InitStatusOfSink()
    {
        if (groundTransform == null)
        {
            enabled = false;
        }
        else
        {
            if (transform.position.y < groundTransform.position.y)
            {
                //Here are objects that are sunk at start
                Debug.Log(gameObject.name + " has sunk at start!");
                hasSunk = true;
                enabled = false;
            }
            else
            {
                hasSunk = false;
            }
        }

    }

    void Update()
    {
        if (!hasSunk && groundTransform != null)
        {
            CheckIfSunk();
        }

    }
    private void CheckIfSunk()
    {
        if (IsSunk())
        {
            CallSunkEvent();
        }
    }

    private bool IsSunk()
    {
        return transform.position.y < initialPosition.y && transform.position.y < groundTransform.position.y;
    }

    private void CallSunkEvent()
    {
        hasSunk = true;
        EventArgument argument = new EventArgument();
        argument.stringComponent = typeOfSinkable;
        EventManager.GetInstance().CallEvent(CustomEvent.SinkHasHappened, argument);
        Debug.Log(typeOfSinkable + " has sunk " + gameObject.name);
        enabled = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

    }
}
