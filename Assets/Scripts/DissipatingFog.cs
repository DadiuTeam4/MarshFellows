// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DissipatingFog : Swipeable 
{
	[Tooltip("How quickly the fog dissipates")]
	[Range(0.01f, 1.0f)]
	public float dissipationSpeed = 0.1f;

	[Tooltip("How much the emissionrate is diminished per tick")]
	[Range(0.01f, 5.0f)]
	public float dissipationRate = 0.1f;

	private Collider collider;
	private ParticleSystem particleSystem;
	private ParticleSystem.EmissionModule particleSysteEmissionModule;
	private IEnumerator dissipate;

	void Start()
	{
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