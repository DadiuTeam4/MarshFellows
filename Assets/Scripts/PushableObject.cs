// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PushableObject : MonoBehaviour 
{
	new Rigidbody rigidbody;

	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();	
	}

	public void Push(Vector3 direction, float force)
	{
		direction.Normalize();
		direction *= force;
		rigidbody.AddForce(direction);
	}
}
