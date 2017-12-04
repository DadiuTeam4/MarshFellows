// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : Singleton<ParticleSpawner> 
{
	private ParticleSystem particleSystem;

	void Start()
	{
		particleSystem = GetComponent<ParticleSystem>();
	}

	public void Burst(Vector3 position)
	{	
		transform.position = position;
		if (!particleSystem.isPlaying)
		{
			particleSystem.Stop();
		}
		particleSystem.Play();
	}
}
