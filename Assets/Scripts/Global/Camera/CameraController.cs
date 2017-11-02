// Author: You Wu
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Events;
public class CameraController : Shakeable
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

    //Variables For Shaking
    public float shakeDuration = 2f;
    private float shakeIntensity = 0.4f;

    private const float COEFFICIENT_FROM_MAGNITUDE_TO_INTENSITY = 0.00005f;
    public bool isShaking = false;
    private Transform cameraTransform;
    public float CameraSpeed = 0.1f;

    void Awake()
    {
        cameraTransform = GetComponent<Transform>();
    }

    void Start()
    {
        isShaking = false;
        offset = transform.position - playerTransform.position;

        EventDelegate eventDelegate = OnCameraSpecialEvent;
        EventManager.GetInstance().AddListener(CustomEvent.CameraMoving, eventDelegate);

    }

    public void OnCameraSpecialEvent(EventArgument argument)
    {
        cameraAdjustableOffset = CameraOffsetOnTrigger;
        cameraAdjustableRotation = CameraRotationOnTrigger; 
    }

    public override void OnShakeBegin(float magnitude)
    {
        shakeIntensity = magnitude * COEFFICIENT_FROM_MAGNITUDE_TO_INTENSITY;
        isShaking = true;
    }

    //Shake duration decides when shaking effects stop after shaking.
    public override void OnShake(float magnitude)
    {
        shakeDuration = 1f;
        float newShakeIntensity = magnitude * COEFFICIENT_FROM_MAGNITUDE_TO_INTENSITY;
        if (newShakeIntensity > shakeIntensity)
        {
            shakeIntensity = newShakeIntensity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFollowingPlayer();
        UpdateShaking();
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

    void UpdateShaking()
    {
        if (isShaking)
        {
            if (cameraTransform == null)
            {
                Debug.LogError("Transform of Camera is NUll!");
            }
            else
            {
                if (shakeDuration > 0)
                {
                    ShakeCamera();
                }
                else
                {
                    StopShakingCamera();
                }
            }
        }

    }

    void ShakeCamera()
    {
        Vector3 intensity = Random.insideUnitSphere * shakeIntensity;
        cameraTransform.position = cameraTransform.position + intensity;
        shakeIntensity -= Time.deltaTime * shakeIntensity / shakeDuration;
        shakeDuration -= Time.deltaTime;
    }

    void StopShakingCamera()
    {
        shakeDuration = 0f;
        isShaking = false;
        isCameraFollwingPlayer = true;
    }

}
