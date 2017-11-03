// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PushableObject : Swipeable 
{
	Rigidbody rigidbody;
	public float forceScalar = 2.0f;

	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();	
	}

	public override void OnSwipe(RaycastHit raycastHit, Vector3 direction) 
	{
		Vector3 force = new Vector3(direction.x, 0, direction.y);
		rigidbody.AddForce(force * forceScalar);
	}
}
