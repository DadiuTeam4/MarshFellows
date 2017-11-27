// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class TeachFog : MonoBehaviour 
{
	public float fadeTime = 0.3f;
	[Space(10)]
	public float startPosX;
	public float endPosX;
	[Space(10)]
	[Range(0f, 10f)]
	public float jumpTime;
	public AnimationCurve speedCurve;
	[Space(10)]
	public bool translateYAxis;
	public float maxHeight = 3f;
	public AnimationCurve yCurve;
	[Space(10)]
	public float timeBetweenJumps;

	private Animator animator;
	private EventManager eventManager;
	private EventDelegate eventDelegate;
	private bool jumping;
	private Vector3 startPosition;
	private new SkinnedMeshRenderer renderer;

	private void Start()
	{
		renderer = GetComponentInChildren<SkinnedMeshRenderer>();
		animator = GetComponentInChildren<Animator>();
		startPosition = transform.position;
	}

	private void Update()
	{
		if (!jumping)
		{
			jumping = true;
			StartCoroutine(Jump());
			StartCoroutine(FadeIn());
		}
	}

	private IEnumerator Jump()
	{
		animator.SetBool("spiritLongJump", true);
		float timeElapsed = 0f;
		float progress = 0f;
		while (timeElapsed < jumpTime)
		{
			timeElapsed += Time.deltaTime;
			progress = speedCurve.Evaluate(timeElapsed / jumpTime);
			float posX = Mathf.Lerp(startPosX, endPosX, progress);
			transform.localPosition = new Vector3(posX, transform.localPosition.y, transform.localPosition.z);
			yield return null;
		}
		StartCoroutine(Cooldown());
	}

	private IEnumerator FadeIn()
	{
		renderer.enabled = true;
		Color fadedColor = renderer.material.color; 
		float timeElapsed = 0f;
		float progress = 0f;
		while (timeElapsed < fadeTime)
		{
			timeElapsed += Time.deltaTime;
			progress = timeElapsed / fadeTime;
			fadedColor.a = progress;
			renderer.material.color = fadedColor;
			yield return null;
		}

	}

	private IEnumerator FadeOut()
	{
		Color fadedColor = renderer.material.color; 
		float timeElapsed = 0f;
		float progress = 0f;
		while (timeElapsed < fadeTime)
		{
			timeElapsed += Time.deltaTime;
			progress = timeElapsed / fadeTime;
			fadedColor.a = 1 - progress;
			renderer.material.color = fadedColor;
			yield return null;
		}
		renderer.enabled = false;
	}

	private IEnumerator Cooldown()
	{
		animator.SetBool("spiritLongJump", false);
		StartCoroutine(FadeOut());
		yield return new WaitForSeconds(timeBetweenJumps);
		jumping = false;
	}

	private void OnGameStarted(EventArgument argument)
	{

	}
}
