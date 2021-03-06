﻿//Author:Tilemachos
//Co-author: You Wu, Itai Yavin
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class SinkableObjectType : ObjectType
{
    private Vector3 initialPosition;
    public Type objectType = Type.Tree;
    private bool hasSunk;

    EventArgument argument = new EventArgument();

    void Start()
    {
        SetupStringValues();

        argument.gameObjectComponent = gameObject;
        argument.stringComponent = GetTypeStringValue(objectType);
        
        initialPosition = transform.position;
        InitStatusOfSink();
    }

    private void InitStatusOfSink()
    {

        if (transform.position.y < 0)
        {
            //Here are objects that are sunk at start
            hasSunk = true;
            enabled = false;
        }
        else
        {
            hasSunk = false;
        }

    }

    void Update()
    {
        if (!hasSunk)
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
        return transform.position.y < initialPosition.y && transform.position.y < 0;
    }

    private void CallSunkEvent()
    {
        hasSunk = true;
        EventManager.GetInstance().CallEvent(CustomEvent.SinkHasHappened, argument);
        enabled = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

    }
}
