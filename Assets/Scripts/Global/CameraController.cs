// Author: You Wu
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Shakeable
{

    //Variable For following the player
    public Transform playerTransform;
    public bool isCameraFollwingPlayer;
    private Vector3 offset;
    public Vector3 cameraAdjustableOffset;
    public Vector3 cameraAdjustableRotation;
    private Vector3 cameraCurrentRotation;

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
        cameraAdjustableOffset = Vector3.zero;
        cameraAdjustableRotation = Vector3.zero;
        cameraCurrentRotation = Vector3.zero;
        offset = transform.position - playerTransform.position;
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
            Quaternion playerRotation;
            if (cameraCurrentRotation != cameraAdjustableRotation)
            {
                Vector3 changedRotation = cameraAdjustableRotation - cameraCurrentRotation;
                cameraCurrentRotation = cameraAdjustableRotation;
                transform.rotation *= Quaternion.Euler(changedRotation.x, changedRotation.y, changedRotation.z);
            }
            playerRotation = Quaternion.Euler(transform.eulerAngles.x, playerTransform.eulerAngles.y, 1f);
            transform.rotation = playerRotation;

            //Kepp the relative position
            Vector3 targetPos = playerTransform.position - (playerRotation * Vector3.forward * offset.magnitude) + cameraAdjustableOffset;
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
