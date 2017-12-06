// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Events;
public class ModelCrossFade : MonoBehaviour 
{
	public CustomEvent transformOnEvent;
	private EventDelegate beginTransformation;
	[Space(5)]

	[Header("Model Fading")]
	[Tooltip("The amount of seconds before crossfade begins")]
	public float secondsBeforeFade = 1.0f;

	[Tooltip("The interval between fade increments in seconds. The fade will be applied each modelFadeRate second.")]
	[Range(0.1f, 1.0f)]
	public float modelFadeRate = 0.1f;
	
	[Tooltip("How much the alpha will be changed each interval.")]
	public float modelFadeIncrement = 0.1f;

	public GameObject fadeOutModel;
	public GameObject fadeInModel;

	private Renderer[] fadeOutModelRenderers;
	private Renderer[] fadeInModelRenderers;
	
	private List<Material> fadeOutModelMaterials;
	private List<Material> fadeInModelMaterials;
	[Space(5)]

	[Header("Animation")]
	public string fadeInAnimationName;
	public string fadeOutAnimationName;

	public float runFadeInAnimationAfterXSeconds = 2;
	public float runFadeOutAnimationAfterXSeconds = 0;

	private Animator fadeInAnimator;
	private Animator fadeOutAnimator;

	private IEnumerator playFadeInAfterWait;
	private IEnumerator playFadeOutAfterWait;

	public GameObject shamanFog;

	void Start () 
	{
		beginTransformation = BeginTransformation;
		EventManager.GetInstance().AddListener(transformOnEvent, beginTransformation);

		fadeInAnimator = fadeInModel.GetComponentInChildren<Animator>();
		fadeOutAnimator = fadeOutModel.GetComponentInChildren<Animator>();

		fadeOutModelRenderers = fadeOutModel.GetComponentsInChildren<Renderer>();
		fadeInModelRenderers = fadeInModel.GetComponentsInChildren<Renderer>();
	
		fadeOutModelMaterials = GetMaterialsFromRendererArray(fadeOutModelRenderers);
		fadeInModelMaterials = GetMaterialsFromRendererArray(fadeInModelRenderers);

		playFadeInAfterWait = WaitToPlayAnimation(fadeInAnimator, fadeInAnimationName, runFadeInAnimationAfterXSeconds);
		playFadeOutAfterWait = WaitToPlayAnimation(fadeOutAnimator, fadeOutAnimationName, runFadeOutAnimationAfterXSeconds);
	}
	
	private List<Material> GetMaterialsFromRendererArray(Renderer[] renderer)
	{
		List<Material> materials = new List<Material>();

		for (int i = 0; i < renderer.Length; i++)
		{
			for (int j = 0; j < renderer[i].materials.Length; j++)
			{
				materials.Add(renderer[i].materials[j]);
			}
		}	

		return materials;
	}

	private void FadeMaterials(List<Material> materials, int directionScalar)
	{
		Color tempColor;

		foreach (Material material in materials)
		{
			tempColor = material.color;
			tempColor.a += (modelFadeIncrement / 256) * directionScalar;

			if (tempColor.a > 1)
			{
				tempColor.a = 1;
			}
			else if (tempColor.a < 0)
			{
				tempColor.a = 0;
			}

			material.color = tempColor;
		}
	}

	private void SetRenderModeOnModel(List<Material> modelMaterials, float renderMode)
	{
		for (int i = 0; i < modelMaterials.Count; i++)
		{
			modelMaterials[i].SetFloat("_Mode", renderMode);
			
			if (renderMode == 2 || renderMode == 3)
			{
				modelMaterials[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
				modelMaterials[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
				modelMaterials[i].SetInt("_ZWrite", 1);
				modelMaterials[i].DisableKeyword("_ALPHATEST_ON");
				modelMaterials[i].EnableKeyword("_ALPHABLEND_ON");
				modelMaterials[i].DisableKeyword("_ALPHAPREMULTIPLY_ON");
				modelMaterials[i].renderQueue = 3000;
			}
			else
			{
				modelMaterials[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
				modelMaterials[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
				modelMaterials[i].SetInt("_ZWrite", 1);
				modelMaterials[i].DisableKeyword("_ALPHATEST_OFF");
				modelMaterials[i].EnableKeyword("_ALPHABLEND_OFF");
				modelMaterials[i].DisableKeyword("_ALPHAPREMULTIPLY_OFF");
				modelMaterials[i].renderQueue = 1000;
			}
		}
	}

	private void BeginTransformation(EventArgument argument)
	{
		StartCoroutine("CrossFade");
		StartCoroutine(playFadeInAfterWait);
		StartCoroutine(playFadeOutAfterWait);
		ShamanFogTransformation();
	}

	private void ShamanFogTransformation()
	{
		shamanFog.SetActive(true);
		shamanFog.GetComponent<ShamanFog>().OnShamanTransformation();
	}

	private IEnumerator WaitToPlayAnimation(Animator animator, string animation, float secondsToWait)
	{
		yield return new WaitForSeconds(secondsToWait);

		animator.SetBool(animation, true);
	}

	private void ScaleModel(GameObject scaledObject, int scaleScalar)
	{
		Vector3 newScale = new Vector3(scaledObject.transform.localScale.x + (modelFadeIncrement * scaleScalar),
										scaledObject.transform.localScale.y + (modelFadeIncrement * scaleScalar),
										scaledObject.transform.localScale.z + (modelFadeIncrement * scaleScalar)); 

		if(newScale.x < 0.01f)
		{
			newScale = new Vector3(0.01f, 0.01f, 0.01f);
		}
		else if (newScale.x > 1.0f)
		{
			newScale = new Vector3(1.0f, 1.0f, 1.0f);
		}

		scaledObject.transform.localScale = newScale;
	}

	private IEnumerator CrossFade()
	{
		yield return new WaitForSeconds(secondsBeforeFade);
		
		/*SetRenderModeOnModel(fadeOutModelMaterials, 2);

		int fadeOutModelScalar = (fadeOutModelMaterials[0].color.a < 0.5) ? 1 : -1;
		int fadeInModelScalar = (fadeInModelMaterials[0].color.a < 0.5) ? 1 : -1;*/

		int fadeOutModelScalar = -1;
		int fadeInModelScalar = 1;

		while (fadeOutModel.transform.localScale.x > 0.01f || fadeInModel.transform.localScale.y < 1.0f)//fadeOutModelMaterials[0].color.a > 0 || fadeInModelMaterials[0].color.a < 1)
		{
			/*FadeMaterials(fadeOutModelMaterials, fadeOutModelScalar);
			FadeMaterials(fadeInModelMaterials, fadeInModelScalar);*/

			ScaleModel(fadeOutModel, fadeOutModelScalar);
			ScaleModel(fadeInModel, fadeInModelScalar);

			yield return new WaitForSeconds(modelFadeRate);
		}
		
		SetRenderModeOnModel(fadeInModelMaterials, 0);
	}

}
