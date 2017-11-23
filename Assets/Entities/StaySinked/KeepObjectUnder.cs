//Author:Tilemachos
//Co-author: You Wu
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
public class KeepObjectUnder : MonoBehaviour
{

    private Collider parentCollider;
    private Collider myCollider;
    private List<GameObject> sinkedObjects = new List<GameObject>();
    void OnTriggerEnter(Collider other)
    {
        if (!sinkedObjects.Contains(other.gameObject))
        {
            sinkedObjects.Add(other.gameObject);
            Rigidbody rigidbody = other.GetComponent<Rigidbody>();
            if (rigidbody)
            {
                rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            }

            SinkableObjectType sinked = other.GetComponent<SinkableObjectType>();

            if (sinked)
            {
                EventManager eventManager = EventManager.GetInstance();

                EventArgument argument = new EventArgument();

                argument.stringComponent = sinked.GetTypeStringValue(sinked.objectType);

                argument.gameObjectComponent = other.gameObject;

                eventManager.CallEvent(CustomEvent.SinkHasHappened, argument);
            }
        }
    }
}
