//Author:Tilemachos	


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWind : Swipeable {

public GameObject windZoneLeft; //the prefab you want to spawn
public GameObject windZoneRight; //the prefab you want to spawn


public GameObject leftParticleSystem;
public GameObject rightParticleSystem;
public float timer = waitTime; 
private ParticleSystem[] leftFog;
private ParticleSystem[] rightFog;
private static float waitTime = 3;
private float defaultEmissionRate = 10;
private float changedEmissionRate = 1;

private bool leftSwipeHasHappened = false;
private bool rightSwipeHasHappened = false;
private Vector3 windDirection;
private GameObject newWind;

	void Start()
	{
		leftParticleSystem = Instantiate(leftParticleSystem) as GameObject;
		rightParticleSystem = Instantiate(rightParticleSystem) as GameObject;

		leftFog = leftParticleSystem.GetComponentsInChildren<ParticleSystem>();
		rightFog = rightParticleSystem.GetComponentsInChildren<ParticleSystem>();

		foreach( ParticleSystem childPS in leftFog )
        {
			childPS.transform.parent = gameObject.transform.parent;
		}

		foreach( ParticleSystem childPS in rightFog )
        {
			childPS.transform.parent = gameObject.transform.parent;
		}
		
	}


	void LateUpdate()
	{
		if(InputSystem.swipeDirections.Count > 0)
		{
			windDirection = InputSystem.swipeDirections[0];
			if(windDirection.x < 0.0)
			{
				leftSwipeHasHappened = true;
			}
			if(windDirection.x > 0.0)
			{
				rightSwipeHasHappened = true;
			}
		}
		timer -= Time.deltaTime;

		if (Input.GetKeyDown("q") || rightSwipeHasHappened)//right wind
		{
			StopEverything();
			
 			foreach( ParticleSystem childPS in leftFog )
            {
				var externalForce = childPS.externalForces;
				externalForce.enabled = false;
			}


  	       	newWind = Instantiate(windZoneRight) as GameObject;

			newWind.transform.parent = gameObject.transform.parent;

			timer = waitTime;
		}

		if (Input.GetKeyDown("e") || leftSwipeHasHappened)//left wind
		{
			StopEverything();
			
 			foreach( ParticleSystem childPS in rightFog )
            {
				var externalForce = childPS.externalForces;
				externalForce.enabled = false;
			}


  	       	newWind = Instantiate(windZoneLeft) as GameObject;

 			newWind.transform.parent = gameObject.transform.parent;

			timer = waitTime;
		}

		if(timer < 0)
		{
			StopEverything();
		}


    }



	void StopEverything()
	{
			Destroy(newWind);
			rightSwipeHasHappened = false;
			leftSwipeHasHappened = false;

			foreach( ParticleSystem childPS in rightFog )
            {
				var externalForce = childPS.externalForces;
				externalForce.enabled = true;
			}
			foreach( ParticleSystem childPS in leftFog )
            {
				var externalForce = childPS.externalForces;
				externalForce.enabled = true;
			}

			timer = waitTime;
	}
	
}
