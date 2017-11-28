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

	void Start()
	{
        oNav = GameObject.FindGameObjectWithTag("O").GetComponent<Navigator>();
        pNav = GameObject.FindGameObjectWithTag("P").GetComponent<Navigator>();
        speedAfterZone = GlobalConstantsManager.GetInstance().constants.speed;
	}

	void OnTriggerEnter(Collider other)
	{
		if (string.Compare(other.transform.name, "P") == 0 && !speedAfterZoneSat)
		{
            speedAfterZone = oNav.GetSpeed();
            oNav.SetSpeed(speedZoneSpeed);
            pNav.SetSpeed(speedZoneSpeed);
            speedAfterZoneSat = true;
        }
    }

	void OnTriggerExit(Collider other)
	{
		if (string.Compare(other.transform.name, "P") == 0)
		{
			oNav.SetSpeed(speedAfterZone);
            pNav.SetSpeed(speedAfterZone);
		}
	}
}
