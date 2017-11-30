// Author: Jonathan
// Contributers: Emil

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HunterSpeedControlZone : MonoBehaviour
{
    public float speedZoneSpeed;
    private float speedAfterZone;
    private bool speedAfterZoneSat;

    private Navigator oNav;
    private Navigator pNav;
    private CatchUp pCatchUp;

    void Start()
    {
        oNav = GameObject.FindGameObjectWithTag("O").GetComponent<Navigator>();
        pCatchUp = GameObject.FindGameObjectWithTag("P").GetComponent<CatchUp>();
        pNav = GameObject.FindGameObjectWithTag("P").GetComponent<Navigator>();
        speedAfterZone = GlobalConstantsManager.GetInstance().constants.speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (string.Compare(other.transform.name, "P") == 0 && !speedAfterZoneSat)
        {
            speedAfterZone = oNav.GetSpeed();
            SetPZoneSpeedBasedOnIfCatching();
            pNav.SetSpeed(speedZoneSpeed);
            speedAfterZoneSat = true;
        }
    }

    private void SetPZoneSpeedBasedOnIfCatching()
    {
        if (pCatchUp.IsCathing())
        {
            oNav.SetSpeed(speedZoneSpeed + GlobalConstantsManager.GetInstance().constants.catchingIncresedSpeed);
        }
        else
        {
            oNav.SetSpeed(speedZoneSpeed);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (string.Compare(other.transform.name, "P") == 0)
        {
            SetPAfterZoneSpeedBasedOnIfCatching();
            pNav.SetSpeed(speedAfterZone);
        }
    }

    private void SetPAfterZoneSpeedBasedOnIfCatching()
    {
        if (pCatchUp.IsCathing())
        {
            oNav.SetSpeed(speedAfterZone + GlobalConstantsManager.GetInstance().constants.catchingIncresedSpeed);
        }
        else
        {
            oNav.SetSpeed(speedAfterZone);
        }
    }

}
