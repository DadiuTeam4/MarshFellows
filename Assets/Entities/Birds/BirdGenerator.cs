// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdGenerator : Singleton<BirdGenerator>
{
	[Header("Flocks")]
	public Flock flockPrefab;
	public int minNumFlocks;
	public int maxNumFlocks;
	public int minDistanceBetweenFlocks;
	public int minBirdsPerFlock = 2;
	public int maxBirdsPerFlock = 10;
	[Space(5)]
	
	public Transform[] spawnPoints;
	public float minDistanceFromSpawn;
	public float maxDistanceFromSpawn;

	[Header("Individual birds")]
	public Bird birdPrefab;
	public float flightAngle = 45f;
	public float flightAngleRandomRange = 20f;
	public float maxY = 30f;
	public AnimationCurve speedCurve;
	public AnimationCurve angleCurve;
	[Space(5)]
	
	public float minDistanceFromFlockCenter = 5f;
	public float maxDistanceFromFlockCenter = 10f;

	private Flock[] flocks;

	private void Start()
	{
		GenerateFlocks();
	}

	private void GenerateFlocks()
	{
		int numFlocks = Random.Range(minNumFlocks, maxNumFlocks);
		flocks = new Flock[numFlocks];
		for (int i = 0; i < numFlocks; i++)
		{
			int spawnPoint = Random.Range(0, spawnPoints.Length);
			float xDirection = Random.Range(0.0f, 1.0f) * 2f - 1f;
			float zDirection = Random.Range(0.0f, 1.0f) * 2f - 1f;

			float x = spawnPoints[spawnPoint].position.x+Random.Range(minDistanceFromSpawn, maxDistanceFromSpawn) * xDirection;
			float y = spawnPoints[spawnPoint].position.y;
			float z = spawnPoints[spawnPoint].position.z+Random.Range(minDistanceFromSpawn, maxDistanceFromSpawn) * zDirection;
		
			flocks[i] = Instantiate(flockPrefab, new Vector3(x, y, z), Quaternion.identity);
		}
		InitializeFlocks();
	}

	private void InitializeFlocks()
	{
		foreach (Flock flock in flocks)
		{
			flock.SpawnBirds();
		}
	}
}
