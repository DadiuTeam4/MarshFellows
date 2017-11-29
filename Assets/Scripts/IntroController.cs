// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class IntroController : Swipeable
{
	public Canvas titleScreenCanvas;
	public float fadeOutTime = 5f;

	private CanvasRenderer canvasRenderer;

	public void Start()
	{
		canvasRenderer = titleScreenCanvas.GetComponent<CanvasRenderer>();
	}

	public override void OnSwipe(RaycastHit raycastHit, Vector3 direction)
	{
		StartCoroutine(FadeOut());
	}

	private IEnumerator FadeOut()
	{
		canvasRenderer.SetAlpha(1);
		float timeElapsed = 0;
		float progress;
		while (timeElapsed < fadeOutTime)
		{
			timeElapsed += Time.deltaTime;
			progress = timeElapsed / fadeOutTime;
			canvasRenderer.SetAlpha(progress);
			yield return null;
		}
		canvasRenderer.SetAlpha(0);
	}
}
