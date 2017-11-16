// Author: You Wu
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFall : MonoBehaviour
{
    private Quaternion initialRotation;
    private bool hasFall;

    void Start()
    {
        initialRotation = transform.rotation;
        hasFall = false;
    }

    void Update()
    {
        if (!hasFall)
        {
            CheckIfFall();
        }

    }

    private void CheckIfFall()
    {
        Quaternion currentRotation = transform.rotation;
        if (Mathf.Abs(currentRotation.eulerAngles.x - initialRotation.eulerAngles.x) > 180 )
        {
			Debug.Log( initialRotation.eulerAngles.x);
			Debug.Log( currentRotation.eulerAngles.x);
			
			Debug.Log(Mathf.Abs(currentRotation.eulerAngles.x - initialRotation.eulerAngles.x));
            Debug.Log(gameObject.name + " Fall");
			//Debug.Log(Mathf.Abs(currentRotation.eulerAngles.z - initialRotation.eulerAngles.z));
			
            hasFall = true;
        }
    }
}
