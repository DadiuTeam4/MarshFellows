// Author: You Wu
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Events;
public class CameraController : MonoBehaviour
{

    //Variable For following the player
    public Transform playerTransform;
    public bool isCameraFollwingPlayer;
    private Vector3 offset;
    public Vector3 cameraAdjustableOffset;
    public Vector3 cameraAdjustableRotation;
    private Vector3 cameraCurrentRotation;

    public Vector3 CameraOffsetOnTrigger;
    public Vector3 CameraRotationOnTrigger;

    private const float COEFFICIENT_FROM_MAGNITUDE_TO_INTENSITY = 0.00005f;
    public bool isShaking = false;

    public float CameraSpeed = 0.1f;

    void Start()
    {
        offset = transform.position - playerTransform.position;

        // EventDelegate eventDelegate = OnCameraSpecialEvent;
        // EventManager.GetInstance().AddListener(CustomEvent.CameraMoving, eventDelegate);

    }

    public void OnCameraSpecialEvent(EventArgument argument)
    {
        cameraAdjustableOffset = CameraOffsetOnTrigger;
        cameraAdjustableRotation = CameraRotationOnTrigger; 
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFollowingPlayer();
    }

    void UpdateFollowingPlayer()
    {
        if (isCameraFollwingPlayer)
        {
            //Ajust Camera rotation onchange
            //Use euler to rotate certain degress in certain axies
            Quaternion newRotation;
            if (cameraCurrentRotation != cameraAdjustableRotation)
            {
                Vector3 changedRotation = cameraAdjustableRotation - cameraCurrentRotation;
                cameraCurrentRotation = cameraAdjustableRotation;
                transform.rotation *= Quaternion.Euler(changedRotation.x, 0f, 0f);
            }
            newRotation = Quaternion.Euler(transform.eulerAngles.x, playerTransform.eulerAngles.y + cameraCurrentRotation.y, cameraCurrentRotation.z);
            transform.rotation = newRotation;

            //Update the new position according to the player position
            Vector3 targetPos = playerTransform.position - (newRotation * Vector3.forward * offset.magnitude) + cameraAdjustableOffset;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, CameraSpeed);
        }
    }

}
