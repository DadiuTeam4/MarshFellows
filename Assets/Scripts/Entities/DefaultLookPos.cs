//Author: Emil Villumsen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultLookPos : MonoBehaviour {


    Vector3 position = new Vector3();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        position.x = Mathf.Sin(Time.time);
        position.y = transform.localPosition.y;
        position.z = transform.localPosition.z;
        transform.localPosition = position;
	}
}
