//Author: Emil Villumsen
//tilemachos
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultLookPos : MonoBehaviour {

	private int round = 0;
	private float nextActionTime = 0.0f;
	public int valueToDivide = 750;
	private float initialY;
	public new string name;

	void Start()
	{
		initialY = transform.localPosition.y;
	}

	// Update is called once per frame
	void Update () 
	{
		nextActionTime += Time.deltaTime;
		
		//2*Pi i.e. full circle
		if(nextActionTime > 2* Mathf.PI)
		{
			round++;
			nextActionTime = 0.0f;
		}
		if(name == "P")
		{
			if(round%2 == 0)
			{
				transform.localPosition = new Vector3(Mathf.Sin(nextActionTime), Mathf.Abs(Mathf.Sin(nextActionTime))/valueToDivide + transform.localPosition.y, transform.localPosition.z);		
			}
			else
			{
				if( transform.localPosition.y - Mathf.Abs(Mathf.Sin(nextActionTime)) > initialY)
				{
					transform.localPosition = new Vector3(Mathf.Sin(nextActionTime), transform.localPosition.y - Mathf.Abs(Mathf.Sin(nextActionTime))/valueToDivide, transform.localPosition.z);		
				}
			}
		}
		else
		{
			if(round%2 == 1)
			{
				transform.localPosition = new Vector3(Mathf.Cos(nextActionTime), transform.localPosition.y - Mathf.Abs(Mathf.Cos(Time.deltaTime)/valueToDivide), transform.localPosition.z);		
			}
			else
			{
				if( transform.localPosition.y + Mathf.Abs(Mathf.Cos(nextActionTime)) < initialY)
				{
					transform.localPosition = new Vector3(Mathf.Cos(nextActionTime), transform.localPosition.y + Mathf.Abs(Mathf.Cos(nextActionTime)), transform.localPosition.z);		
				}
			}
		}
	}
}
