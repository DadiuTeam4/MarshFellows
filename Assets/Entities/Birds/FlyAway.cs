using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAway : MonoBehaviour 
{
	private bool flying;
	[SerializeField]
	Bird[] birds;
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
