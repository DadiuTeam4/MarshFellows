// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraControl;

[RequireComponent(typeof(Collider))]
public class CameraFocusObject : MonoBehaviour
{
    private CameraStateController mainCamera;
    private Collider myCollider;

    private void Start()
    {
        mainCamera = CameraStateController.GetInstance();
        myCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "O" && mainCamera.GetTrackedObject() == null)
        {
            mainCamera.SetTrackedObject(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "O" && mainCamera.GetTrackedObject() == transform)
        {
            mainCamera.SetTrackedObject(null);
            myCollider.enabled = false;
        }
    }
}
