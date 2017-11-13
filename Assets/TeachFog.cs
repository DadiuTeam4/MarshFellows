// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachFog : MonoBehaviour 
{
	Vector3 startPos;

	void Start()
	{
		startPos = transform.position;
	}

	void FixedUpdate() 
	{
		float posX = startPos.x + Mathf.Sin(Time.time * 10) * 10;
		transform.position = new Vector3(posX, transform.position.y, transform.position.z);		
	}
}
