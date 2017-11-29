// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GlobalConstants
{
	[Header("Hunter")]
	public float speed = 1.4f;
	public float acceleration = 8;
	public float radius = 0.5f;
	public float height = 2.0f;

	[Header("Fog")]
	public bool on = true;
	public float fogDensity = 0.7f;
	public Color fogColor = new Color(255.0f, 255.0f, 255.0f, 1.0f);
	public FogMode fogMode = FogMode.ExponentialSquared;

	[Header("Audio")]
	public float sFXVolume = 100; 
	public float musicVolume = 100; 
	
	[Header("Third Person Camera")]
	public float xRotation = 25;
	public float fieldOfView = 38;
}
