// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : Singleton<ParticleSpawner> 
{
	private ParticleSystem particleSystem;
	private ParticleSystemRenderer renderer;

	void Start()
	{
		particleSystem = GetComponent<ParticleSystem>();
		renderer = particleSystem.GetComponent<ParticleSystemRenderer>();
	}

	public void Burst(Vector3 position, Material material)
	{	
		transform.position = position;
		renderer.material = material;
		if (!particleSystem.isPlaying)
		{
			particleSystem.Stop();
		}
		particleSystem.Play();
	}
}
