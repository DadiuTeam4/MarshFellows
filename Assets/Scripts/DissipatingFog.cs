// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(Collider))]
public class DissipatingFog : Swipeable 
{
	[Tooltip("An attached navmesh obstacle component will only be disabled if this is true!")]
	public bool isNavmeshObstacle = false;

	[Tooltip("How quickly the fog dissipates")]
	[Range(0.001f, 1.0f)]
	public float dissipationSpeed = 0.1f;

	[Tooltip("How much the emissionrate is diminished per tick")]
	[Range(0.01f, 30.0f)]
	public float dissipationRate = 0.1f;

	private new Collider collider;
	private new ParticleSystem particleSystem;
	private ParticleSystem.EmissionModule particleSysteEmissionModule;

	private ParticleSystem childParticleSystem;
	private ParticleSystem.EmissionModule childParticleSysteEmissionModule;

	private IEnumerator dissipate;
	private IEnumerator dissipateChild;
	private NavMeshObstacle obstacle;

	void Start()
	{
		if (isNavmeshObstacle)
		{
			obstacle = GetComponent<NavMeshObstacle>();
			if (obstacle)
			{
				obstacle.enabled = true;
			}
		}
		
		collider = GetComponent<Collider>();
		particleSystem = GetComponent<ParticleSystem>();
		particleSysteEmissionModule = particleSystem.emission;

		if (transform.childCount > 0)
		{
			childParticleSystem = transform.GetChild(0).GetComponent<ParticleSystem>();
			childParticleSysteEmissionModule = childParticleSystem.emission;
			dissipateChild = Dissipate(childParticleSysteEmissionModule);
		}
		dissipate = Dissipate(particleSysteEmissionModule);
	}


	public override void OnSwipe(RaycastHit hit, Vector3 direction)
	{
		if (collider)
		{
			collider.enabled = false;
			if (isNavmeshObstacle && obstacle)
			{
				obstacle.enabled = false;
			}
		}

		StartCoroutine(dissipate);

		if (childParticleSystem)
		{
			StartCoroutine(dissipateChild);
		}
	}

	private IEnumerator Dissipate(ParticleSystem.EmissionModule module)
	{
		float currentRate = module.rateOverTime.constant;

		while (module.rateOverTime.constant > 0)
		{
			currentRate -= dissipationRate;
			module.rateOverTime = currentRate;

			yield return new WaitForSeconds(dissipationSpeed);
		}
	}
}