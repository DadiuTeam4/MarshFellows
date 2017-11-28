// Author: Peter Jæger
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour 
{
	
	private bool triggered;

    [Space]
    [Header("Reminder to scale the box collider to fit the whole area the color change is active")]
    [Space]

	[Header("Lerp from current colors to these colors")]
	[SerializeField]
	private Color sky;
	[SerializeField]
	private Color equator;
	[SerializeField]
	private Color ground;

	private Color skyStart;
	private Color equatorStart;
	private Color groundStart;

	[SerializeField]
	[Range(0,1.0f)]
	private float changeMultiplier = 1.0f;

	[Header("Positions of the start and end, should be on the path")]
	[SerializeField]
	private Transform start;
	[SerializeField]
	private Transform end;
	private Transform hunter;
	
	private void LateUpdate () 
	{
		if( triggered)
		{
			UpdateHunterPosition(hunter);
		}
	}

	private void UpdateHunterPosition(Transform hunter)
	{
		float distanceFromBeginningToHunters = Vector3.Distance(hunter.position, start.position);
		float distanceFromEndToHunters = Vector3.Distance(hunter.position, end.position);
		float progress = distanceFromBeginningToHunters / (distanceFromBeginningToHunters + distanceFromEndToHunters);
		ChangeColor(progress * changeMultiplier);
	}

	private void OnTriggerEnter(Collider other)
	{
		if( other.name == "P")
		{
			hunter = other.transform;
			triggered = true;
			skyStart = RenderSettings.ambientSkyColor;
			equatorStart = RenderSettings.ambientEquatorColor;
			groundStart = RenderSettings.ambientGroundColor;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if( other.name == "P")
		{
			Destroy(this.gameObject);	
		}
	}

	private void ChangeColor(float progress)
	{
		RenderSettings.ambientSkyColor = Color.Lerp(skyStart, sky, progress);
		RenderSettings.ambientEquatorColor = Color.Lerp(equatorStart, equator, progress);
		RenderSettings.ambientGroundColor = Color.Lerp(groundStart, ground, progress);
	}

}
