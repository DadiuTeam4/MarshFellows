//Author:Tilemachos
//Co-author:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepObjectUnder : MonoBehaviour {

private Collider parentCollider;
private Collider myCollider;

	void OnTriggerEnter(Collider other)
	{
		Rigidbody rigidbody = other.GetComponent<Rigidbody>();
		if (rigidbody)
		{
			rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY |RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		}
	}
}
