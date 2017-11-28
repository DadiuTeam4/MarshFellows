// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(NavMeshObstacle))]
public class DissipatingFog : Swipeable 
{
	[Tooltip("An attached navmesh obstacle component will only be disabled if this is true!")]
	public bool isNavmeshObstacle = false;

	[Tooltip("How quickly the fog dissipates")]
	[Range(0.01f, 1.0f)]
	public float dissipationSpeed = 0.1f;

	[Tooltip("How much the emissionrate is diminished per tick")]
	[Range(0.01f, 5.0f)]
	public float dissipationRate = 0.1f;

	private new Collider collider;
	private new ParticleSystem particleSystem;
	private ParticleSystem.EmissionModule particleSysteEmissionModule;
	private IEnumerator dissipate;
	private NavMeshObstacle obstacle;

	void Start()
	{
		if (isNavmeshObstacle)
		{
			obstacle = GetComponent<NavMeshObstacle>();
			obstacle.enabled = true;
		}
		
		collider = GetComponent<Collider>();
		particleSystem = GetComponent<ParticleSystem>();
		particleSysteEmissionModule = particleSystem.emission;
		
		dissipate = Dissipate();
	}


	public override void OnSwipe(RaycastHit hit, Vector3 direction)
	{
		if (collider)
		{
			collider.enabled = false;
			if (isNavmeshObstacle)
			{
				obstacle.enabled = false;
			}
		}

		StartCoroutine(dissipate);
	}

	private IEnumerator Dissipate()
	{
		float currentRate = particleSysteEmissionModule.rateOverTime.constant;
		while (particleSysteEmissionModule.rateOverTime.constant > 0)
		{
			currentRate -= dissipationRate;
			particleSysteEmissionModule.rateOverTime = currentRate;

			yield return new WaitForSeconds(dissipationSpeed);
		}
	}
}