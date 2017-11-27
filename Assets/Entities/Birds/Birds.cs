using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birds : MonoBehaviour 
{
	private bool flying;
	private Bird birdPrefab;
	private Bird[] birds;

	private void Awake()
	{
		
	}

	private void Start()
	{
		int numBirds = Random.Range(GlobalConstantsManager.GetInstance().constants.minBirds, GlobalConstantsManager.GetInstance().constants.maxBirds);
		birds = new Bird[numBirds];
		for (int i = 0; i < numBirds; i++)
		{
			GameObject bird = Instantiate(birdPrefab);	
			birds[i] = bird.GetComponent<Bird>();
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
