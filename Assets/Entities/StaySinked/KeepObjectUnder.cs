//Author:Tilemachos
//Co-author:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
public class KeepObjectUnder : MonoBehaviour {

private Collider parentCollider;
private Collider myCollider;


	void OnTriggerEnter(Collider other)
	{
		Rigidbody rigidbody = other.GetComponent<Rigidbody>();
		rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY |RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		
		SinkableObjectType sinked = other.GetComponent<SinkableObjectType>();
		
		EventManager eventManager = EventManager.GetInstance();

		EventArgument argument = new EventArgument();

		argument.stringComponent = sinked.typeOfSinkable;

		argument.gameObjectComponent = other.gameObject;

        eventManager.CallEvent(CustomEvent.SinkHasHappened,argument);

	}
}
