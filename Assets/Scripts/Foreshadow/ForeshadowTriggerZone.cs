// Author: Jonathan
// Contributers:

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForeshadowTriggerZone : MonoBehaviour
{
	public GameObject foreshadowEvent;

	void OnTriggerEnter(Collider other)
	{
		//if (string.Compare(other.transform.name, "O") == 0)
		{
			Debug.Log("Triggered!");
			Instantiate(foreshadowEvent, transform.position, transform.rotation);
		}
	}
}
