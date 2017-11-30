// Author: You Wu
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Navigator))]
public class CatchUp : MonoBehaviour
{

    private Navigator oNav;
    private Transform pTransform;
    private bool isCatching;
    public int seperationDistance = 6;

    void Start()
    {
        oNav = GetComponent<Navigator>();
        isCatching = false;
        CheckNotNull();
    }

    private void CheckNotNull()
    {
        if (!pTransform)
        {
            Debug.LogError("Need P Transfrom to Catch Up!");
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
            if (!CheckIfSeperated())
            {
                isCatching = true;
                BackToNomalSpeed();
            }
        }
    }

    private void SetCatchingSpeed()
    {
        oNav.SetSpeed(oNav.GetSpeed() * 2);
    }

    private void BackToNomalSpeed()
    {
        oNav.SetSpeed(oNav.GetSpeed() * 0.5f);
    }

    private bool CheckIfSeperated()
    {
        float currentDistance = Vector3.Distance(transform.position, pTransform.position);
        return currentDistance > seperationDistance;
    }
}
