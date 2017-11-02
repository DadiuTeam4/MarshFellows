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
    private Vector3 relativePos;

    public Vector3 CameraAdjustableOffset;

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
        relativePos = playerTransform.InverseTransformPoint(transform.position);
        isShaking = false;
        CameraAdjustableOffset = Vector3.zero;
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
            //Rotate the camera
            //transform.LookAt(playerTransform);
            Quaternion playerRotation = Quaternion.Euler(transform.eulerAngles.x, playerTransform.eulerAngles.y, 0);
            transform.rotation = playerRotation;
            Vector3 targetPos = playerTransform.TransformPoint(relativePos) + CameraAdjustableOffset;
            //Kepp the relative position
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
