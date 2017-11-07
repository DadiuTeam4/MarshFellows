using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventPoster : MonoBehaviour {
	
	public float maxTime = 15;
	public float minTime = 8;
	//current time
	private float time;
	//The time to spawn the event
	private float postTime;
	public string eventName; 

	void Start()
	{
		SetRandomTime();
		time = minTime;
		eventName = string.Concat ("", eventName, ""); 

	}
	void FixedUpdate()
	{
		//Counts up
		time += Time.deltaTime;
		//Check if its the right time to post the event
		if(time >= postTime)
		{
			PostEvent();
			SetRandomTime();
		}
	}
	//Spawns the object and resets the time
	void PostEvent()
	{
		time = 2;
		AkSoundEngine.PostEvent (eventName, gameObject); 
	}
	//Sets the random time between minTime and maxTime
	void SetRandomTime()
	{
		postTime = Random.Range(minTime, maxTime);
	}
}

