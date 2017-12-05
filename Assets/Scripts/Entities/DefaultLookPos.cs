//Author: Emil Villumsen
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultLookPos : MonoBehaviour {

	private int round = 0;
	private bool firstRoundFlag =true;
	private float nextActionTime = 0.0f;
	private int valueToDivide = 500;
	private float initialY;

	void Start()
	{
		initialY = transform.localPosition.y;
	}

	// Update is called once per frame
	void Update () {
		if(Time.time > 3.0f && firstRoundFlag)
		{
			round++;
			nextActionTime += Time.time;
			firstRoundFlag =false;
		}
		if(Time.time - nextActionTime > 3.0f && nextActionTime - nextActionTime < 3.5f)
		{
			round++;
			nextActionTime += Time.time;
		}
		if(gameObject.name == "P")
		{
			if(round%2 == 0)
			{
				transform.localPosition = new Vector3(Mathf.Sin(Time.time), Mathf.Abs(Mathf.Sin(Time.time))/valueToDivide + transform.localPosition.y, transform.localPosition.z);		
			}
			else
			{
				if( transform.localPosition.y - Mathf.Abs(Mathf.Sin(Time.time)) > initialY)
				{
					transform.localPosition = new Vector3(Mathf.Sin(Time.time), transform.localPosition.y - Mathf.Abs(Mathf.Sin(Time.time))/valueToDivide, transform.localPosition.z);		
				}
			}
		}
		else
		{
			if(round%2 == 0)
			{
				transform.localPosition = new Vector3(Mathf.Sin(Time.time), transform.localPosition.y - Mathf.Abs(Mathf.Sin(Time.time)/valueToDivide), transform.localPosition.z);		
			}
			else
			{
				if( transform.localPosition.y + Mathf.Abs(Mathf.Sin(Time.time)) < initialY)
				{
					transform.localPosition = new Vector3(Mathf.Sin(Time.time), transform.localPosition.y + Mathf.Abs(Mathf.Sin(Time.time)), transform.localPosition.z);		
				}
			}
		}

		
	}
}
