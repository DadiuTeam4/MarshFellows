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
	private float catchingIncresedSpeed;
	

    void Start()
    {
        oNav = GameObject.FindGameObjectWithTag("O").GetComponent<Navigator>();
        pCatchUp = GameObject.FindGameObjectWithTag("P").GetComponent<CatchUp>();
        pNav = GameObject.FindGameObjectWithTag("P").GetComponent<Navigator>();
        speedAfterZone = GlobalConstantsManager.GetInstance().constants.speed;
		catchingIncresedSpeed = GlobalConstantsManager.GetInstance().constants.catchingIncresedSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (string.Compare(other.transform.name, "P") == 0 && !speedAfterZoneSat)
        {
            SetPZoneSpeedBasedOnIfCatching();
            oNav.SetSpeed(speedZoneSpeed);
            speedAfterZoneSat = true;
        }
    }

    private void SetPZoneSpeedBasedOnIfCatching()
    {
        if (pCatchUp.IsCathing())
        {
            pNav.SetSpeed(speedZoneSpeed + catchingIncresedSpeed);
        }
        else
        {
            pNav.SetSpeed(speedZoneSpeed);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (string.Compare(other.transform.name, "P") == 0)
        {
            SetPAfterZoneSpeedBasedOnIfCatching();
            oNav.SetSpeed(speedAfterZone);
        }
    }

    private void SetPAfterZoneSpeedBasedOnIfCatching()
    {
        if (pCatchUp.IsCathing())
        {
            pNav.SetSpeed(speedAfterZone + catchingIncresedSpeed);
        }
        else
        {
            pNav.SetSpeed(speedAfterZone);
        }
    }

}
