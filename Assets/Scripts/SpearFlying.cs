// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Events;
public class SpearFlying : MonoBehaviour 
{
	[Tooltip("This is compared with the argument from the chosen event, if they arent equal, then the spear wont fly.")]
	public string owner;

	[Tooltip("This gives the spear a curve in the y direction")]
	public AnimationCurve yCurve;
	[Tooltip("A scalar multiplied to the yCurve so as to strengthen the curve")]
	[Range(0.5f, 5.0f)]
	public float yCurveScalar = 1.0f;
	[Tooltip("The event which triggers the spear flying")]
	public CustomEvent flyOnEvent;
	[Tooltip("The event which is called when the spear hits its target")]
	public CustomEvent onHitEvent;

	private Transform target;

	[Tooltip("How quickly the spear will reach its target destination")]
	[Range(0.01f, 1.0f)]
	public float lerpRate = 0.01f;

	[Tooltip("How often the spear will be moved forward, in seconds")]
	[Range(0.01f, 1.0f)]
	public float incrementRate = 0.01f;

    [Tooltip("Delay before throwing spear, in seconds. Match with shoot animation")]
    [Range(0.00f, 3.0f)]
    public float throwDelay = 0.8f;

    private float lerpT = 0;
	private EventDelegate spearLerp;

	private bool spearThrown = false;

	void Start()
	{
		spearLerp = SetupForSpearLerp;
		EventManager.GetInstance().AddListener(flyOnEvent, spearLerp);
	}

	private void SetupForSpearLerp(EventArgument argument)
	{
		if (!owner.Equals(argument.stringComponent))
		{
			return;
		}

		target = argument.gameObjectComponent.transform;

		if (!spearThrown)
		{

			StartCoroutine("SpearLerp");
		}
	}

	private IEnumerator SpearLerp()
	{
        yield return new WaitForSeconds(throwDelay);
        transform.parent = null;
        Vector3 thrownPosition = transform.position;
		Vector3 yOffsetPosition;

		while (lerpT != 1)
		{
			Debug.DrawLine(thrownPosition, target.position, Color.red, incrementRate);

			transform.LookAt(target.position);

			lerpT += lerpRate * Time.deltaTime * 60.0f;
			if (lerpT > 1)
			{
				lerpT = 1;

				EventManager.GetInstance().CallEvent(onHitEvent);

				transform.parent = target;
			}

			yOffsetPosition = Vector3.Lerp(thrownPosition, target.position, lerpT);
			yOffsetPosition.y += yCurve.Evaluate(lerpT);

			transform.position = yOffsetPosition;

			yield return new WaitForSeconds(incrementRate);
		}
	}
}
