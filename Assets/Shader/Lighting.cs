// Author: Peter Jæger
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour 
{
	
	public Color sky;
	public Color equator;
	public Color ground;

	public float changeSpeed = 0.001f;

	public Transform start;
	public Transform end;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKey(KeyCode.A))
		{
			ChangeColor();
		}	

	}


	void OnTriggerEnter(Collider other)
	{
		//float distance = 
	}


	void ChangeColor()
	{
		Color AmbSky = Color.Lerp(RenderSettings.ambientSkyColor, sky, (Mathf.PingPong(Time.time, 1)*changeSpeed));
		Color AmbEquator = Color.Lerp(RenderSettings.ambientEquatorColor, equator, (Mathf.PingPong(Time.time, 1)*changeSpeed));
		Color AmbGround = Color.Lerp(RenderSettings.ambientGroundColor, ground, (Mathf.PingPong(Time.time, 1)*changeSpeed));


		RenderSettings.ambientSkyColor = AmbSky;
		RenderSettings.ambientEquatorColor = AmbEquator;
		RenderSettings.ambientGroundColor = AmbGround;
	}

}
