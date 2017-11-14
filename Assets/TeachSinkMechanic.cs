// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachSinkMechanic : MonoBehaviour 
{
	Vector3 startPos;

	void Start()
	{
		startPos = transform.position;
	}

	void FixedUpdate() 
	{
		float posZ = startPos.z + Mathf.Sin(Time.time * 5) * 5;
		transform.position = new Vector3(transform.position.x, transform.position.y, posZ);		
	}
}
