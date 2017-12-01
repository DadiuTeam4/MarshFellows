// Author: You Wu
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Navigator))]
public class CatchUp : MonoBehaviour
{

    private Navigator pNav;
    private Transform oTransform;
    private bool isCatching;
    public float seperationDistance = 3;
    public float reunitedDistance = 2;
    private float catchingIncresedSpeed;

    void Start()
    {
        pNav = GetComponent<Navigator>();
        oTransform = GameObject.FindGameObjectWithTag("O").GetComponent<Transform>();
        isCatching = false;
        catchingIncresedSpeed = GlobalConstantsManager.GetInstance().constants.catchingIncresedSpeed;
        CheckNotNull();
    }

    private void CheckNotNull()
    {
        if (!oTransform)
        {
            Debug.LogError("Cannot Find O Transform!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfNeedCatching();
        CheckIfNeedStopCatching();
    }

    private void CheckIfNeedCatching()
    {
        if (!isCatching)
        {
            if (CheckIfSeperated())
            {
                isCatching = true;
                SetCatchingSpeed();
            }
        }
    }

    private void CheckIfNeedStopCatching()
    {
        if (isCatching)
        {
            if (CheckIfReunited())
            {
                isCatching = false;
                BackToNomalSpeed();
            }
        }
    }

    private void SetCatchingSpeed()
    {
        pNav.SetSpeed(pNav.GetSpeed() + catchingIncresedSpeed);
    }

    private void BackToNomalSpeed()
    {
        pNav.SetSpeed(pNav.GetSpeed() - catchingIncresedSpeed);
    }

    private bool CheckIfSeperated()
    {
        return GetCurrentDistance() > seperationDistance;
    }

    private bool CheckIfReunited()
    {
        return GetCurrentDistance() < reunitedDistance;
    }

    private float GetCurrentDistance()
    {
        return Vector3.Distance(transform.position, oTransform.position);
    }

    public bool IsCathing()
    {
        return isCatching;
    }

}
