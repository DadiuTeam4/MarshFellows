// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class TeachFog : MonoBehaviour 
{
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

	private EventManager eventManager;
	private EventDelegate eventDelegate;
	private bool jumping;
	private Vector3 startPosition;

	// private void Awake()
	// {
	// 	eventDelegate += OnGameStarted;
	// }

	// private void OnEnable()
	// {
	// 	eventManager = EventManager.GetInstance();
	// 	eventManager.AddListener(CustomEvent.GameStarted, eventDelegate);
	// }

	private void Start()
	{
		startPosition = transform.position;
	}

	private void Update()
	{
		if (!jumping)
		{
			jumping = true;
			StartCoroutine(Jump());
		}
	}

	private IEnumerator Jump()
	{
		float timeElapsed = 0f;
		float progress = 0f;
		while (timeElapsed < jumpTime)
		{
			timeElapsed += Time.deltaTime;
			progress = speedCurve.Evaluate(timeElapsed / jumpTime);
			float posX = Mathf.Lerp(startPosX, endPosX, progress);
			float posY = transform.position.y;
			if (translateYAxis)
			{
				posY = yCurve.Evaluate(progress);
			}
			transform.position = new Vector3(posX, transform.position.y, transform.position.z);
			yield return null;
		}
		StartCoroutine(Cooldown());
	}

	private IEnumerator Cooldown()
	{
		yield return new WaitForSeconds(timeBetweenJumps);
		jumping = false;
	}

	private void OnGameStarted(EventArgument argument)
	{

	}
}
