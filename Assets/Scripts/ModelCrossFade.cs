// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelCrossFade : MonoBehaviour 
{
	[Tooltip("The interval between fade increments in seconds. The fade will be applied each modelFadeRate second.")]
	[Range(0.1f, 1.0f)]
	public float modelFadeRate = 0.1f;
	
	[Tooltip("How much the alpha will be changed each interval.")]
	[Range(1.0f, 25.0f)]
	public float modelFadeIncrement = 5.0f;

	public GameObject model1;
	public GameObject model2;

	private Renderer[] model1Renderers;
	private Renderer[] model2Renderers;
	
	private Material[] model1Materials;
	private Material[] model2Materials;

	void Start () 
	{
		model1Renderers = model1.GetComponentsInChildren<Renderer>();
		model2Renderers = model2.GetComponentsInChildren<Renderer>();
	
		model1Materials = GetMaterialsFromRendererArray(model1Renderers);
		model2Materials = GetMaterialsFromRendererArray(model2Renderers);

		StartCoroutine("CrossFade");	
	}
	
	private Material[] GetMaterialsFromRendererArray(Renderer[] renderer)
	{
		Material[] materials = new Material[renderer.Length];

		for (int i = 0; i < renderer.Length; i++)
		{
			materials[i] = renderer[i].material;
		}	

		return materials;
	}

	private void FadeMaterials(Material[] materials, int directionScalar)
	{
		Color tempColor;

		foreach (Material material in materials)
		{
			tempColor = material.color;
			tempColor.a += modelFadeIncrement * directionScalar;
			material.color = tempColor;
		}
	}

	private IEnumerator CrossFade()
	{
		int model1Scalar = (model1Materials[0].color.a < 128) ? 1 : -1;
		int model2Scalar = (model2Materials[0].color.a < 128) ? 1 : -1;

		while ((model1Materials[0].color.a >= 256 || model1Materials[0].color.a <= 0) 
			&& (model2Materials[0].color.a >= 256 || model2Materials[0].color.a <= 0))
		{
			FadeMaterials(model1Materials, model1Scalar);
			FadeMaterials(model2Materials, model2Scalar);

			yield return new WaitForSeconds(modelFadeRate);
		}
	}

}
