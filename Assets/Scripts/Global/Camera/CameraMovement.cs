// Author: You Wu
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform playerTransform;
    private Transform cameraTransform;
    Vector3 offset;
    public float CameraSpeed = 0.1f;

	private Vector3 relativePos;

    void Awake()
    {
        cameraTransform = GetComponent<Transform>();
        if(playerTransform == null)
        {
            Debug.LogError("Player Transform in Camera Movement Class Should not be Null!");
        }
    }

    void Start()
    {
         offset = transform.position - playerTransform.position;
		 relativePos = playerTransform.InverseTransformPoint(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
		follingPlayer();
    }

	private void follingPlayer()
	{
		//Rotate the camera
		transform.LookAt(playerTransform);
		//Kepp the relative position
		transform.position = playerTransform.TransformPoint(relativePos);
	}
}