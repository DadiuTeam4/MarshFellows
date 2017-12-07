// Author: You Wu
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ShamanFog : MonoBehaviour
{

    [Tooltip("How quickly the fog dissipates")]
    [Range(0.001f, 1.0f)]
    public float dissipationSpeed = 0.1f;

    [Tooltip("How much the emissionrate is diminished per tick")]
    [Range(0.01f, 30.0f)]
    public float dissipationRate = 0.1f;

    private new ParticleSystem particleSystem;
    private ParticleSystem.EmissionModule particleSysteEmissionModule;

    private ParticleSystem childParticleSystem;
    private ParticleSystem.EmissionModule childParticleSysteEmissionModule;

    private IEnumerator dissipate;
    private IEnumerator dissipateChild;

    void Start()
    {
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

    public void OnShamanTransformation()
    {
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
