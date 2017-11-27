// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GlobalConstants/New Settings Script")]
public class GlobalConstants : ScriptableObject
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
	public float sfxVolume = 100; 
	public float musicVolume = 100; 

	[Header("Procedural birds (whoah amazing)")]
	public float flightAngle = 45;
	public float flightAngleRandomRange = 20;
	public float maxY = 30;
	public AnimationCurve speedCurve;
	public AnimationCurve angleCurve;
	public int minBirds = 2;
	public int maxBirds = 10;
}
