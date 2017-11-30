// Author: Mathias Dam Hedelund
// Contributors: Itai Yavin
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour 
{
	private bool flying;
	private Bird[] birds;
	private BirdGenerator birdGenerator;

	private void Start()
	{
		birdGenerator = BirdGenerator.GetInstance();
		SpawnBirds();
	}

	public void SpawnBirds()
	{
		int numBirds = Random.Range(birdGenerator.minBirdsPerFlock, birdGenerator.maxBirdsPerFlock);
		birds = new Bird[numBirds];

		for (int i = 0; i < numBirds; i++)
		{
			// Randomize spawn directions
			float xDirection = Random.Range(0.0f, 1.0f) * 2f - 1f;
			float zDirection = Random.Range(0.0f, 1.0f) * 2f - 1f;

			// Generate position in flock
			float x = transform.position.x 
				+ Random.Range(birdGenerator.minDistanceFromFlockCenter
				, birdGenerator.maxDistanceFromFlockCenter) 
				* xDirection;

			float y = transform.position.y;

			float z = transform.position.z
				+ Random.Range(birdGenerator.minDistanceFromFlockCenter
				, birdGenerator.maxDistanceFromFlockCenter)
				* zDirection;

			// Spawn bird
			birds[i] = Instantiate(birdGenerator.birdPrefab, new Vector3(x, y, z), Quaternion.identity, transform).GetComponent<Bird>();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!flying)
		{
			if (other.gameObject.CompareTag("ScenarioTrigger"))
			{
				Vector3 direction = transform.position - other.transform.position;
				foreach (Bird bird in birds)
				{
					bird.Fly(direction);
				}
				flying = true;
			}
		}
	}
}
