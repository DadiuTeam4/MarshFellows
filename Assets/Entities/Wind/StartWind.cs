﻿//Author:Tilemachos	


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWind : Swipeable {

public GameObject windZoneLeft; //the prefab you want to spawn
public GameObject windZoneRight; //the prefab you want to spawn
//public ParticleSystem middleFogLeft;
//public ParticleSystem middleFogRight;

//private ParticleSystem bottomFogRight;
//private ParticleSystem bottomFogLeft;

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
		
	}


	void LateUpdate()
	{
		if(InputSystem.swipeDirections.Count > 0)
		{
			windDirection = InputSystem.swipeDirections[0];
			print(windDirection);
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
			
			//var externalForce = middleFogLeft.externalForces;
        	//externalForce.enabled = false;
 			foreach( ParticleSystem childPS in leftFog )
            {
				var externalForce = childPS.externalForces;
				externalForce.enabled = false;
			}


			//externalForce = bottomFogLeft.externalForces;
        	//externalForce.enabled = false;

			//externalForce = middleFogRight.externalForces;
        	//externalForce.enabled = false;	
		
			//var emissionRate = middleFogRight.emission;
			//emissionRate.rateOverTime = changedEmissionRate;

  	       	newWind = Instantiate(windZoneRight) as GameObject;
			timer = waitTime;
		}

		if (Input.GetKeyDown("e") || leftSwipeHasHappened)//left wind
		{
			StopEverything();
			
			//var externalForce = middleFogRight.externalForces;
        	//externalForce.enabled = false;

			//externalForce = middleFogLeft.externalForces;
        	//externalForce.enabled = false;
 			foreach( ParticleSystem childPS in rightFog )
            {
				var externalForce = childPS.externalForces;
				externalForce.enabled = false;
			}


			//var emissionRate = middleFogLeft.emission;
			//emissionRate.rateOverTime = changedEmissionRate;

  	       	newWind = Instantiate(windZoneLeft) as GameObject;
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
			//var externalForce = middleFogRight.externalForces;
        	//externalForce.enabled = true;

			//var externalForce = topFogRight.externalForces;
        	//externalForce.enabled = true;

			//externalForce = bottomFogRight.externalForces;
        	//externalForce.enabled = true;

			//externalForce = middleFogLeft.externalForces;
        	//externalForce.enabled = true;

			//externalForce = topFogLeft.externalForces;
        	//externalForce.enabled = true;

			//externalForce = bottomFogLeft.externalForces;
        	//externalForce.enabled = true;

			//var emissionRate = middleFogRight.emission;
			//emissionRate.rateOverTime = defaultEmissionRate;

			//emissionRate = middleFogLeft.emission;
			//emissionRate.rateOverTime = defaultEmissionRate;

			timer = waitTime;
	}
	
}
