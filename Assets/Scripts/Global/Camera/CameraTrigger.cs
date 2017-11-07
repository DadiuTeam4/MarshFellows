// Author: You Wu
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Events;
public class CameraTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		//EventManager.GetInstance().CallEvent(CustomEvent.CameraMoving, new EventArgument());
	}


}
